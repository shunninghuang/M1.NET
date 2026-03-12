using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using mame;

namespace cpu.hd6309
{
    public unsafe partial class Hd6309 : cpuexec_data
    {
        public static Hd6309[] mm1;
        public Action[] hd6309_main, hd6309_page01, hd6309_page11;
        public byte[] cycle_counts_page0,cycle_counts_page01,cycle_counts_page11,index_cycle;
        public Register pc, ppc, d, w, dp, u, s, x, y, v, ea;
        public byte z8, dummy_byte;
        public ushort z16;
        public byte cc, md, ireg;
        public byte[] bitTable = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
        public LineState[] irq_state = new LineState[2];
        public int extra_cycles;
        public byte int_state;
        public LineState nmi_state;
        private byte CC_C = 0x01, CC_V = 0x02, CC_Z = 0x04, CC_N = 0x08, CC_II = 0x10, CC_H = 0x20, CC_IF = 0x40, CC_E = 0x80;
        private byte MD_EM = 0x01, MD_FM = 0x02, MD_II = 0x40, MD_DZ = 0x80;
        public Func<ushort, byte> ReadOp, ReadOpArg;
        public Func<ushort, byte> RM;
        public Action<ushort, byte> WM;
        public delegate int irq_delegate(int irqline);
        public irq_delegate irq_callback;
        public delegate void debug_delegate();
        public debug_delegate debugger_start_cpu_hook_callback, debugger_stop_cpu_hook_callback;
        private ulong totalExecutedCycles;
        private int pendingCycles;
        public override ulong TotalExecutedCycles
        {
            get
            {
                return totalExecutedCycles;
            }
            set
            {
                totalExecutedCycles = value;
            }
        }
        public override int PendingCycles
        {
            get
            {
                return pendingCycles;
            }
            set
            {
                pendingCycles = value;
            }
        }
        public byte[] flags8i = new byte[256]
{
0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x08|0x02,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08
};
        public byte[] flags8d = new byte[256]
{
0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,
0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08,0x08
};
        public byte[] index_cycle_em = new byte[256] {        /* Index Loopup cycle counts, emulated 6809 */
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */

/* 0x0X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x1X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x2X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x3X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x4X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x5X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x6X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x7X */      1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
/* 0x8X */      2,    3,    2,    3,    0,    1,    1,    1,    1,    4,    1,    4,    1,    5,    4,    0,
/* 0x9X */      3,    6,   20,    6,    3,    4,    4,    4,    4,    7,    4,    7,    4,    8,    7,    5,
/* 0xAX */      2,    3,    2,    3,    0,    1,    1,    1,    1,    4,    1,    4,    1,    5,    4,    5,
/* 0xBX */      5,    6,   20,    6,    3,    4,    4,    4,    4,    7,    4,    7,    4,    8,    7,   20,
/* 0xCX */      2,    3,    2,    3,    0,    1,    1,    1,    1,    4,    1,    4,    1,    5,    4,    3,
/* 0xDX */      4,    6,   20,    6,    3,    4,    4,    4,    4,    7,    4,    7,    4,    8,    7,   20,
/* 0xEX */      2,    3,    2,    3,    0,    1,    1,    1,    1,    4,    1,    4,    1,    5,    4,    3,
/* 0xFX */      4,    6,   20,    6,    3,    4,    4,    4,    4,    7,    4,    7,    4,    8,    7,   20
};
        public byte[] index_cycle_na = new byte[256] {         /* Index Loopup cycle counts,
native 6309 */
/*       X0, X1, X2, X3, X4, X5, X6, X7, X8, X9, XA, XB, XC, XD, XE, XF */

/* 0x0X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x1X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x2X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x3X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x4X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x5X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x6X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x7X */   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
/* 0x8X */   1,  2,  1,  2,  0,  1,  1,  1,  1,  3,  1,  2,  1,  3,  1,  0,
/* 0x9X */   3,  5, 19,  5,  3,  4,  4,  4,  4,  7,  4,  5,  4,  6,  4,  5,
/* 0xAX */   1,  2,  1,  2,  0,  1,  1,  1,  1,  3,  1,  2,  1,  3,  1,  2,
/* 0xBX */   5,  5, 19,  5,  3,  4,  4,  4,  4,  7,  4,  5,  4,  6,  4, 19,
/* 0xCX */   1,  2,  1,  2,  0,  1,  1,  1,  1,  3,  1,  2,  1,  3,  1,  1,
/* 0xDX */   4,  5, 19,  5,  3,  4,  4,  4,  4,  7,  4,  5,  4,  6,  4, 19,
/* 0xEX */   1,  2,  1,  2,  0,  1,  1,  1,  1,  3,  1,  2,  1,  3,  1,  1,
/* 0xFX */   4,  5, 19,  5,  3,  4,  4,  4,  4,  7,  4,  5,  4,  6,  4, 19
};
        public byte[] ccounts_page0_em = new byte[256]   /* Cycle Counts Page zero, Emulated 6809 */
{
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */
/* 0x0X */     6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    3,    6,
/* 0x1X */     0,    0,    2,    4,    4,   19,    5,    9,   19,    2,    3,   19,    3,    2,    8,    6,
/* 0x2X */     3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,
/* 0x3X */     4,    4,    4,    4,    5,    5,    5,    5,   19,    5,    3,    6,   20,   11,   19,   19,
/* 0x4X */     2,   19,   19,    2,    2,   19,    2,    2,    2,    2,    2,   19,    2,    2,   19,    2,
/* 0x5X */     2,   19,   19,    2,    2,   19,    2,    2,    2,    2,    2,   19,    2,    2,   19,    2,
/* 0x6X */     6,    7,    7,    6,    6,    6,    6,    6,    6,    6,    6,    7,    6,    6,    3,    6,
/* 0x7X */     7,    7,    7,    7,    7,    7,    7,    7,    7,    7,    7,    5,    7,    7,    4,    7,
/* 0x8X */     2,    2,    2,    4,    2,    2,    2,   19,    2,    2,    2,    2,    4,    7,    3,   19,
/* 0x9X */     4,    4,    4,    6,    4,    4,    4,    4,    4,    4,    4,    4,    6,    7,    5,    5,
/* 0xAX */     4,    4,    4,    6,    4,    4,    4,    4,    4,    4,    4,    4,    6,    7,    5,    5,
/* 0xBX */     5,    5,    5,    7,    5,    5,    5,    5,    5,    5,    5,    5,    7,    8,    6,    6,
/* 0xCX */     2,    2,    2,    4,    2,    2,    2,   19,    2,    2,    2,    2,    3,    5,    3,   19,
/* 0xDX */     4,    4,    4,    6,    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,    5,    5,
/* 0xEX */     4,    4,    4,    6,    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,    5,    5,
/* 0xFX */     5,    5,    5,    7,    5,    5,    5,    5,    5,    5,    5,    5,    6,    6,    6,    6
};
        public byte[] ccounts_page0_na = new byte[256]  /* Cycle Counts Page zero, Native 6309 */
{
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */
/* 0x0X */     5,    6,    6,    5,    5,    6,    5,    5,    5,    5,    5,    6,    5,    4,    2,    5,
/* 0x1X */     0,    0,    1,    4,    4,   19,    4,    7,   19,    1,    2,   19,    3,    1,    5,    4,
/* 0x2X */     3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,
/* 0x3X */     4,    4,    4,    4,    4,    4,    4,    4,   19,    4,    1,    6,   22,   10,   19,   21,
/* 0x4X */     1,   19,   19,    1,    1,   19,    1,    1,    1,    1,    1,   19,    1,    1,   19,    1,
/* 0x5X */     1,   19,   19,    1,    1,   19,    1,    1,    1,    1,    1,   19,    1,    1,   19,    1,
/* 0x6X */     6,    7,    7,    6,    6,    6,    6,    6,    6,    6,    6,    7,    6,    5,    3,    6,
/* 0x7X */     6,    7,    7,    6,    6,    7,    6,    6,    6,    6,    6,    5,    6,    5,    3,    6,
/* 0x8X */     2,    2,    2,    3,    2,    2,    2,   19,    2,    2,    2,    2,    3,    6,    3,   19,
/* 0x9X */     3,    3,    3,    4,    3,    3,    3,    3,    3,    3,    3,    3,    4,    6,    4,    4,
/* 0xAX */     4,    4,    4,    5,    4,    4,    4,    4,    4,    4,    4,    4,    5,    6,    5,    5,
/* 0xBX */     4,    4,    4,    5,    4,    4,    4,    4,    4,    4,    4,    4,    5,    7,    5,    5,
/* 0xCX */     2,    2,    2,    3,    2,    2,    2,   19,    2,    2,    2,    2,    3,    5,    3,   19,
/* 0xDX */     3,    3,    3,    4,    3,    3,    3,    3,    3,    3,    3,    3,    4,    4,    4,    4,
/* 0xEX */     4,    4,    4,    5,    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,    5,    5,
/* 0xFX */     4,    4,    4,    5,    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,    5,    5
};
        public byte[] ccounts_page01_em = new byte[256]   /* Cycle Counts Page 01, Emulated 6809 */
{
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */
/* 0x0X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x1X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x2X */     20,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,
/* 0x3X */      4,    4,    4,    4,    4,    4,    4,    4,    6,    6,    6,    6,   20,   20,   20,   20,
/* 0x4X */      2,    20,  20,    2,    2,   20,    2,    2,    2,    2,    2,   20,    2,    2,   20,    2,
/* 0x5X */     20,   20,   20,    3,    3,   20,    3,   20,   20,    3,    3,   20,    3,    3,   20,    3,
/* 0x6X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x7X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x8X */      5,    5,    5,    5,    5,    5,    4,   20,    5,    5,    5,    5,    5,   20,    4,   20,
/* 0x9X */      7,    7,    7,    7,    7,    7,    6,    6,    7,    7,    7,    7,    7,   20,    6,    6,
/* 0xAX */      7,    7,    7,    7,    7,    7,    6,    6,    7,    7,    7,    7,    7,   20,    6,    6,
/* 0xBX */      8,    8,    8,    8,    8,    8,    7,    7,    8,    8,    8,    8,    8,   20,    7,    7,
/* 0xCX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    4,   20,
/* 0xDX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    8,    8,    6,    6,
/* 0xEX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    8,    8,    6,    6,
/* 0xFX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    9,    9,    7,    7
};
        public byte[] ccounts_page01_na = new byte[256]  /* Cycle Counts Page 01, Native 6309 */
{
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */
/* 0x0X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x1X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x2X */     20,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,
/* 0x3X */      4,    4,    4,    4,    4,    4,    4,    4,    6,    6,    6,    6,   20,   20,   20,   22,
/* 0x4X */      1,   20,   20,    1,    1,   20,    1,    1,    1,    1,    1,   20,    1,    1,   20,    1,
/* 0x5X */     20,   20,   20,    2,    2,   20,    2,   20,   20,    2,    2,   20,    2,    2,   20,    1,
/* 0x6X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x7X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x8X */      4,    4,    4,    4,    4,    4,    4,   20,    4,    4,    4,    4,    4,   20,    4,   20,
/* 0x9X */      5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,   20,    5,    5,
/* 0xAX */      6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,   20,    6,    6,
/* 0xBX */      6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,   20,    6,    6,
/* 0xCX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    4,   20,
/* 0xDX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    7,    7,    5,    5,
/* 0xEX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    8,    8,    6,    6,
/* 0xFX */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,    8,    8,    6,    6
};
        public byte[] ccounts_page11_em = new byte[256]   /* Cycle Counts Page 11, Emulated 6809 */
{
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */
/* 0x0X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x1X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x2X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x3X */      7,    7,    7,    7,    7,    7,    7,    8,    3,    3,    3,    3,    4,    5,   20,   20,
/* 0x4X */     20,   20,   20,    2,   20,   20,   20,   20,   20,   20,    2,   20,    2,    2,   20,    2,
/* 0x5X */     20,   20,   20,    2,   20,   20,   20,   20,   20,   20,    2,   20,    2,    2,   20,    2,
/* 0x6X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x7X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x8X */      3,    3,   20,    5,   20,   20,    3,   20,   20,   20,   20,    3,    5,   25,   34,   28,
/* 0x9X */      5,    5,   20,    7,   20,   20,    5,    5,   20,   20,   20,    5,    7,   27,   36,   30,
/* 0xAX */      5,    5,   20,    7,   20,   20,    5,    5,   20,   20,   20,    5,    7,   27,   36,   30,
/* 0xBX */      6,    6,   20,    8,   20,   20,    6,    6,   20,   20,   20,    6,    8,   28,   37,   31,
/* 0xCX */      3,    3,   20,   20,   20,   20,    3,   20,   20,   20,   20,    3,   20,   20,   20,   20,
/* 0xDX */      5,    5,   20,   20,   20,   20,    5,    5,   20,   20,   20,    5,   20,   20,   20,   20,
/* 0xEX */      5,    5,   20,   20,   20,   20,    5,    5,   20,   20,   20,    5,   20,   20,   20,   20,
/* 0xFX */      6,    6,   20,   20,   20,   20,    6,    6,   20,   20,   20,    6,   20,   20,   20,   20
};
        public byte[] ccounts_page11_na = new byte[256]   /* Cycle Counts Page 11, Native 6309 */
{
/*           0xX0, 0xX1, 0xX2, 0xX3, 0xX4, 0xX5, 0xX6, 0xX7, 0xX8, 0xX9, 0xXA, 0xXB, 0xXC, 0xXD, 0xXE, 0xXF */
/* 0x0X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x1X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x2X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x3X */      6,    6,    6,    6,    6,    6,    6,    7,    3,    3,    3,    3,    4,    5,   20,   22,
/* 0x4X */     20,   20,   20,    2,   20,   20,   20,   20,   20,   20,    2,   20,    2,    2,   20,    2,
/* 0x5X */     20,   20,   20,    2,   20,   20,   20,   20,   20,   20,    2,   20,    2,    2,   20,    2,
/* 0x6X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x7X */     20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,   20,
/* 0x8X */      3,    3,   20,    4,   20,   20,    3,   20,   20,   20,   20,    3,    4,   25,   34,   28,
/* 0x9X */      4,    4,   20,    5,   20,   20,    4,    4,   20,   20,   20,    4,    5,   26,   35,   29,
/* 0xAX */      5,    5,   20,    6,   20,   20,    5,    5,   20,   20,   20,    5,    6,   27,   36,   30,
/* 0xBX */      5,    5,   20,    6,   20,   20,    5,    5,   20,   20,   20,    5,    6,   27,   36,   30,
/* 0xCX */      3,    3,   20,   20,   20,   20,    3,   20,   20,   20,   20,    3,   20,   20,   20,   20,
/* 0xDX */      4,    4,   20,   20,   20,   20,    4,    4,   20,   20,   20,    4,   20,   20,   20,   20,
/* 0xEX */      5,    5,   20,   20,   20,   20,    5,    5,   20,   20,   20,    5,   20,   20,   20,   20,
/* 0xFX */      5,    5,   20,   20,   20,   20,    5,    5,   20,   20,   20,    5,   20,   20,   20,   20
};
        public Hd6309()
        {
            hd6309_main = new Action[0x100] {
/*          0xX0,   0xX1,     0xX2,    0xX3,    0xX4,    0xX5,    0xX6,    0xX7,
            0xX8,   0xX9,     0xXA,    0xXB,    0xXC,    0xXD,    0xXE,    0xXF   */

/* 0x0X */  neg_di,  oim_di,  aim_di,  com_di,  lsr_di,  eim_di,  ror_di,  asr_di,
            asl_di,  rol_di,  dec_di,  tim_di,  inc_di,  tst_di,  jmp_di,  clr_di,

/* 0x1X */  pref10,  pref11,  nop,     sync,    sexw,    IIError, lbra,    lbsr,
            IIError, daa,     orcc,    IIError, andcc,   sex,     exg,     tfr,

/* 0x2X */  bra,     brn,     bhi,     bls,     bcc,     bcs,     bne,     beq,
            bvc,     bvs,     bpl,     bmi,     bge,     blt,     bgt,     ble,

/* 0x3X */  leax,    leay,    leas,    leau,    pshs,    puls,    pshu,    pulu,
            IIError, rts,     abx,     rti,     cwai,    mul,     IIError, swi,

/* 0x4X */  nega,    IIError, IIError, coma,    lsra,    IIError, rora,    asra,
            asla,    rola,    deca,    IIError, inca,    tsta,    IIError, clra,

/* 0x5X */  negb,    IIError, IIError, comb,    lsrb,    IIError, rorb,    asrb,
            aslb,    rolb,    decb,    IIError, incb,    tstb,    IIError, clrb,

/* 0x6X */  neg_ix,  oim_ix,  aim_ix,  com_ix,  lsr_ix,  eim_ix,  ror_ix,  asr_ix,
            asl_ix,  rol_ix,  dec_ix,  tim_ix,  inc_ix,  tst_ix,  jmp_ix,  clr_ix,

/* 0x7X */  neg_ex,  oim_ex,  aim_ex,  com_ex,  lsr_ex,  eim_ex,  ror_ex,  asr_ex,
            asl_ex,  rol_ex,  dec_ex,  tim_ex,  inc_ex,  tst_ex,  jmp_ex,  clr_ex,

/* 0x8X */  suba_im, cmpa_im, sbca_im, subd_im, anda_im, bita_im, lda_im,  IIError,
            eora_im, adca_im, ora_im,  adda_im, cmpx_im, bsr,     ldx_im,  IIError,

/* 0x9X */  suba_di, cmpa_di, sbca_di, subd_di, anda_di, bita_di, lda_di,  sta_di,
            eora_di, adca_di, ora_di,  adda_di, cmpx_di, jsr_di,  ldx_di,  stx_di,

/* 0xAX */  suba_ix, cmpa_ix, sbca_ix, subd_ix, anda_ix, bita_ix, lda_ix,  sta_ix,
            eora_ix, adca_ix, ora_ix,  adda_ix, cmpx_ix, jsr_ix,  ldx_ix,  stx_ix,

/* 0xBX */  suba_ex, cmpa_ex, sbca_ex, subd_ex, anda_ex, bita_ex, lda_ex,  sta_ex,
            eora_ex, adca_ex, ora_ex,  adda_ex, cmpx_ex, jsr_ex,  ldx_ex,  stx_ex,

/* 0xCX */  subb_im, cmpb_im, sbcb_im, addd_im, andb_im, bitb_im, ldb_im,  IIError,
            eorb_im, adcb_im, orb_im,  addb_im, ldd_im,  ldq_im,  ldu_im,  IIError,

/* 0xDX */  subb_di, cmpb_di, sbcb_di, addd_di, andb_di, bitb_di, ldb_di,  stb_di,
            eorb_di, adcb_di, orb_di,  addb_di, ldd_di,  std_di,  ldu_di,  stu_di,

/* 0xEX */  subb_ix, cmpb_ix, sbcb_ix, addd_ix, andb_ix, bitb_ix, ldb_ix,  stb_ix,
            eorb_ix, adcb_ix, orb_ix,  addb_ix, ldd_ix,  std_ix,  ldu_ix,  stu_ix,

/* 0xFX */  subb_ex, cmpb_ex, sbcb_ex, addd_ex, andb_ex, bitb_ex, ldb_ex,  stb_ex,
            eorb_ex, adcb_ex, orb_ex,  addb_ex, ldd_ex,  std_ex,  ldu_ex,  stu_ex
};
            hd6309_page01 = new Action[0x100] {
/*          0xX0,   0xX1,     0xX2,    0xX3,    0xX4,    0xX5,    0xX6,    0xX7,
            0xX8,   0xX9,     0xXA,    0xXB,    0xXC,    0xXD,    0xXE,    0xXF   */

/* 0x0X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x1X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x2X */  IIError, lbrn,    lbhi,    lbls,    lbcc,    lbcs,    lbne,    lbeq,
			lbvc,    lbvs,    lbpl,    lbmi,    lbge,    lblt,    lbgt,    lble,

/* 0x3X */  addr_r,  adcr,    subr,    sbcr,    andr,    orr,     eorr,    cmpr,
			pshsw,   pulsw,   pshuw,   puluw,   IIError, IIError, IIError, swi2,

/* 0x4X */  negd,    IIError, IIError, comd,    lsrd,    IIError, rord,    asrd,
			asld,    rold,    decd,    IIError, incd,    tstd,    IIError, clrd,

/* 0x5X */  IIError, IIError, IIError, comw,    lsrw,    IIError, rorw,    IIError,
			IIError, rolw,    decw,    IIError, incw,    tstw,    IIError, clrw,

/* 0x6X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x7X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x8X */  subw_im, cmpw_im, sbcd_im, cmpd_im, andd_im, bitd_im, ldw_im,  IIError,
			eord_im, adcd_im, ord_im,  addw_im, cmpy_im, IIError, ldy_im,  IIError,

/* 0x9X */  subw_di, cmpw_di, sbcd_di, cmpd_di, andd_di, bitd_di, ldw_di,  stw_di,
			eord_di, adcd_di, ord_di,  addw_di, cmpy_di, IIError, ldy_di,  sty_di,

/* 0xAX */  subw_ix, cmpw_ix, sbcd_ix, cmpd_ix, andd_ix, bitd_ix, ldw_ix,  stw_ix,
			eord_ix, adcd_ix, ord_ix,  addw_ix, cmpy_ix, IIError, ldy_ix,  sty_ix,

/* 0xBX */  subw_ex, cmpw_ex, sbcd_ex, cmpd_ex, andd_ex, bitd_ex, ldw_ex,  stw_ex,
			eord_ex, adcd_ex, ord_ex,  addw_ex, cmpy_ex, IIError, ldy_ex,  sty_ex,

/* 0xCX */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, lds_im,  IIError,

/* 0xDX */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, ldq_di,  stq_di,  lds_di,  sts_di,

/* 0xEX */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, ldq_ix,  stq_ix,  lds_ix,  sts_ix,

/* 0xFX */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, ldq_ex,  stq_ex,  lds_ex,  sts_ex
};
            hd6309_page11 = new Action[0x100] {
/*          0xX0,   0xX1,     0xX2,    0xX3,    0xX4,    0xX5,    0xX6,    0xX7,
            0xX8,   0xX9,     0xXA,    0xXB,    0xXC,    0xXD,    0xXE,    0xXF   */

/* 0x0X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x1X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x2X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x3X */  band,    biand,   bor,     bior,    beor,    bieor,   ldbt,    stbt,
			tfmpp,   tfmmm,   tfmpc,   tfmcp,   bitmd_im,ldmd_im, IIError, swi3,

/* 0x4X */  IIError, IIError, IIError, come,    IIError, IIError, IIError, IIError,
			IIError, IIError, dece,    IIError, ince,    tste,    IIError, clre,

/* 0x5X */  IIError, IIError, IIError, comf,    IIError, IIError, IIError, IIError,
			IIError, IIError, decf,    IIError, incf,    tstf,    IIError, clrf,

/* 0x6X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x7X */  IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,
			IIError, IIError, IIError, IIError, IIError, IIError, IIError, IIError,

/* 0x8X */  sube_im, cmpe_im, IIError, cmpu_im, IIError, IIError, lde_im,  IIError,
			IIError, IIError, IIError, adde_im, cmps_im, divd_im, divq_im, muld_im,

/* 0x9X */  sube_di, cmpe_di, IIError, cmpu_di, IIError, IIError, lde_di,  ste_di,
			IIError, IIError, IIError, adde_di, cmps_di, divd_di, divq_di, muld_di,

/* 0xAX */  sube_ix, cmpe_ix, IIError, cmpu_ix, IIError, IIError, lde_ix,  ste_ix,
			IIError, IIError, IIError, adde_ix, cmps_ix, divd_ix, divq_ix, muld_ix,

/* 0xBX */  sube_ex, cmpe_ex, IIError, cmpu_ex, IIError, IIError, lde_ex,  ste_ex,
			IIError, IIError, IIError, adde_ex, cmps_ex, divd_ex, divq_ex, muld_ex,

/* 0xCX */  subf_im, cmpf_im, IIError, IIError, IIError, IIError, ldf_im,  IIError,
			IIError, IIError, IIError, addf_im, IIError, IIError, IIError, IIError,

/* 0xDX */  subf_di, cmpf_di, IIError, IIError, IIError, IIError, ldf_di,  stf_di,
			IIError, IIError, IIError, addf_di, IIError, IIError, IIError, IIError,

/* 0xEX */  subf_ix, cmpf_ix, IIError, IIError, IIError, IIError, ldf_ix,  stf_ix,
			IIError, IIError, IIError, addf_ix, IIError, IIError, IIError, IIError,

/* 0xFX */  subf_ex, cmpf_ex, IIError, IIError, IIError, IIError, ldf_ex,  stf_ex,
			IIError, IIError, IIError, addf_ex, IIError, IIError, IIError, IIError

};
        }
        public override void Reset()
        {
            hd6309_reset();
        }
        private byte IMMBYTE()
        {
            byte b = ReadOpArg(pc.LowWord);
            pc.LowWord++;
            return b;
        }
        private Register IMMWORD()
        {
            Register w = new Register();
            w.d = (uint)((ReadOpArg(pc.LowWord) << 8) | ReadOpArg((ushort)((pc.LowWord + 1) & 0xffff)));
            pc.LowWord += 2;
            return w;
        }
        private Register IMMLONG()
        {
            Register w = new Register();
            w.d = (uint)((ReadOpArg(pc.LowWord) << 24) + (ReadOpArg((ushort)(pc.LowWord + 1)) << 16) + (ReadOpArg((ushort)(pc.LowWord + 2)) << 8) + (ReadOpArg((ushort)(pc.LowWord + 3))));
            pc.LowWord += 4;
            return w;
        }
        private void PUSHBYTE(byte b)
        {
            --s.LowWord;
            WM(s.LowWord, b);
        }
        private void PUSHWORD(Register w)
        {
            --s.LowWord;
            WM(s.LowWord, w.LowByte);
            --s.LowWord;
            WM(s.LowWord, w.HighByte);
        }
        private byte PULLBYTE()
        {
            byte b;
            b = RM(s.LowWord);
            s.LowWord++;
            return b;
        }
        private ushort PULLWORD()
        {
            ushort w;
            w = (ushort)(RM(s.LowWord) << 8);
            s.LowWord++;
            w |= RM(s.LowWord);
            s.LowWord++;
            return w;
        }
        private void PSHUBYTE(byte b)
        {
            --u.LowWord; WM(u.LowWord, b);
        }
        private void PSHUWORD(Register w)
        {
            --u.LowWord;
            WM(u.LowWord, w.LowByte);
            --u.LowWord;
            WM(u.LowWord, w.HighByte);
        }
        private byte PULUBYTE()
        {
            byte b;
            b = RM(u.LowWord);
            u.LowWord++;
            return b;
        }
        private ushort PULUWORD()
        {
            ushort w;
            w = (ushort)(RM(u.LowWord) << 8);
            u.LowWord++;
            w |= RM(u.LowWord);
            u.LowWord++;
            return w;
        }
        private void CLR_HNZVC()
        {
            cc &= (byte)(~(CC_H | CC_N | CC_Z | CC_V | CC_C));
        }
        private void CLR_NZV()
        {
            cc &= (byte)(~(CC_N | CC_Z | CC_V));
        }
        private void CLR_NZ()
        {
            cc &= (byte)(~(CC_N | CC_Z));
        }
        private void CLR_HNZC()
        {
            cc &= (byte)(~(CC_H | CC_N | CC_Z | CC_C));
        }
        private void CLR_NZVC()
        {
            cc &= (byte)(~(CC_N | CC_Z | CC_V | CC_C));
        }
        private void CLR_Z()
        {
            cc &= (byte)(~(CC_Z));
        }
        private void CLR_N()
        {
            cc &= (byte)(~(CC_N));
        }
        private void CLR_NZC()
        {
            cc &= (byte)(~(CC_N | CC_Z | CC_C));
        }
        private void CLR_ZC()
        {
            cc &= (byte)(~(CC_Z | CC_C));
        }
        private void SET_Z(uint a)
        {
            if (a == 0)
            {
                SEZ();
            }
        }
        private void SET_Z8(byte a)
        {
            if (a == 0)
            {
                SEZ();
            }
        }
        private void SET_Z16(ushort a)
        {
            if (a == 0)
            {
                SEZ();
            }
        }
        private void SET_N8(byte a)
        {
            cc |= (byte)((a & 0x80) >> 4);
        }
        private void SET_N16(ushort a)
        {
            cc |= (byte)((a & 0x8000) >> 12);
        }
        private void SET_N32(uint a)
        {
            cc |= (byte)((a & 0x8000) >> 20);
        }
        private void SET_H(byte a, byte b, byte r)
        {
            cc |= (byte)(((a ^ b ^ r) & 0x10) << 1);
        }
        private void SET_C8(ushort a)
        {
            cc |= (byte)((a & 0x100) >> 8);
        }
        private void SET_C16(uint a)
        {
            cc |= (byte)((a & 0x10000) >> 16);
        }
        private void SET_V8(byte a, ushort b, ushort r)
        {
            cc |= (byte)(((a ^ b ^ r ^ (r >> 1)) & 0x80) >> 6);
        }
        private void SET_V16(ushort a, ushort b, uint r)
        {
            cc |= (byte)(((a ^ b ^ r ^ (r >> 1)) & 0x8000) >> 14);
        }
        private void SET_FLAGS8I(byte a)
        {
            cc |= flags8i[(a) & 0xff];
        }
        private void SET_FLAGS8D(byte a)
        {
            cc |= flags8d[(a) & 0xff];
        }
        private void SET_NZ8(byte a)
        {
            SET_N8(a);
            SET_Z(a);
        }
        private void SET_NZ16(ushort a)
        {
            SET_N16(a);
            SET_Z(a);
        }
        private void SET_FLAGS8(byte a, ushort b, ushort r)
        {
            SET_N8((byte)r);
            SET_Z8((byte)r);
            SET_V8(a, b, r);
            SET_C8(r);
        }
        private void SET_FLAGS16(ushort a, ushort b, uint r)
        {
            SET_N16((ushort)r);
            SET_Z16((ushort)r);
            SET_V16(a, b, r);
            SET_C16(r);
        }
        private byte NXORV()
        {
            return (byte)((cc & CC_N) ^ ((cc & CC_V) << 2));
        }
        private ushort SIGNED(byte b)
        {
            return (ushort)((b & 0x80) != 0 ? b | 0xff00 : b);
        }
        private uint SIGNED_16(ushort b)
        {
            return (uint)((b & 0x8000) != 0 ? b | 0xffff0000 : b);
        }
        private void DIRECT()
        {
            ea.d = dp.d;
            ea.LowByte = IMMBYTE();
        }
        private void IMM8()
        {
            ea.d = pc.d;
            pc.LowWord++;
        }
        private void IMM16()
        {
            ea.d = pc.d;
            pc.LowWord += 2;
        }
        private void EXTENDED()
        {
            ea = IMMWORD();
        }
        private void SEC()
        {
            cc |= CC_C;
        }
        private void CLC()
        {
            cc &= (byte)(~CC_C);
        }
        private void SEZ()
        {
            cc |= CC_Z;
        }
        private void CLZ()
        {
            cc &= (byte)(~CC_Z);
        }
        private void SEN()
        {
            cc |= CC_N;
        }
        private void CLN()
        {
            cc &= (byte)(~CC_N);
        }
        private void SEV()
        {
            cc |= CC_V;
        }
        private void CLV()
        {
            cc &= (byte)(~CC_V);
        }
        private void SEH()
        {
            cc |= CC_H;
        }
        private void CLH()
        {
            cc &= (byte)(~CC_H);
        }
        private void SEDZ()
        {
            md |= MD_DZ;
        }
        private void CLDZ()
        {
            md &= (byte)(~MD_DZ);
        }
        private void SEII()
        {
            md |= MD_II;
        }
        private void CLII()
        {
            md &= (byte)(~MD_II);
        }
        private void SEFM()
        {
            md |= MD_FM;
        }
        private void CLFM()
        {
            md &= (byte)(~MD_FM);
        }
        private void SEEM()
        {
            md |= MD_EM;
        }
        private void CLEM()
        {
            md &= (byte)(~MD_EM);
        }
        private byte DIRBYTE()
        {
            DIRECT();
            return RM(ea.LowWord);
        }
        private Register DIRWORD()
        {
            Register w = new Register();
            DIRECT();
            w.LowWord = RM16(ea.LowWord);
            return w;
        }
        private Register DIRLONG()
        {
            Register lng = new Register();
            DIRECT();
            lng.HighWord = RM16(ea.LowWord);
            lng.LowWord = RM16((ushort)(ea.LowWord + 2));
            return lng;
        }
        private byte EXTBYTE()
        {
            EXTENDED();
            return RM(ea.LowWord);
        }
        private Register EXTWORD()
        {
            Register w = new Register();
            EXTENDED();
            w.LowWord = RM16(ea.LowWord);
            return w;
        }
        private Register EXTLONG()
        {
            Register lng = new Register();
            EXTENDED();
            lng.HighWord = RM16(ea.LowWord);
            lng.LowWord = RM16((ushort)(ea.LowWord + 2));
            return lng;
        }
        private void BRANCH(bool f)
        {
            byte t = IMMBYTE();
            if (f)
            {
                pc.LowWord += (ushort)SIGNED(t);
            }
        }
        private void LBRANCH(bool f)
        {
            Register t = IMMWORD();
            if (f)
            {
                if ((md & MD_EM) == 0)
                {
                    pendingCycles -= 1;
                }
                pc.LowWord += t.LowWord;
            }
        }
        private ushort RM16(ushort Addr)
        {
            ushort result = (ushort)(RM(Addr) << 8);
            return (ushort)(result | RM((ushort)((Addr + 1) & 0xffff)));
        }
        private uint RM32(ushort Addr)
        {
            uint result = (uint)(RM(Addr) << 24);
            result += (uint)(RM((ushort)(Addr + 1)) << 16);
            result += (uint)(RM((ushort)(Addr + 2)) << 8);
            result += (uint)RM((ushort)(Addr + 3));
            return result;
        }
        private void WM16(ushort Addr, Register p)
        {
            WM(Addr, p.HighByte);
            WM((ushort)((Addr + 1) & 0xffff), p.LowByte);
        }
        private void WM32(ushort Addr, Register p)
        {
            WM(Addr, p.HighByte3);
            WM((ushort)((Addr + 1) & 0xffff), p.HighByte2);
            WM((ushort)((Addr + 2) & 0xffff), p.HighByte);
            WM((ushort)((Addr + 3) & 0xffff), p.LowByte);
        }
        private void UpdateState()
        {
            if ((md & MD_EM) != 0)
            {
                cycle_counts_page0 = ccounts_page0_na;
                cycle_counts_page01 = ccounts_page01_na;
                cycle_counts_page11 = ccounts_page11_na;
                index_cycle = index_cycle_na;
            }
            else
            {
                cycle_counts_page0 = ccounts_page0_em;
                cycle_counts_page01 = ccounts_page01_em;
                cycle_counts_page11 = ccounts_page11_em;
                index_cycle = index_cycle_em;
            }
        }
        private void CHECK_IRQ_LINES()
        {
            if (irq_state[0] != LineState.CLEAR_LINE || irq_state[1] != LineState.CLEAR_LINE)
            {
                int_state &= unchecked((byte)(~16));
            }
            if (irq_state[1] != LineState.CLEAR_LINE && (cc & CC_IF)==0)
            {
                if ((int_state & 8) != 0)
                {
                    int_state &= unchecked((byte)~8);
                    extra_cycles += 7;
                }
                else
                {
                    if ((md & MD_FM) != 0)
                    {
                        cc |= CC_E;
                        PUSHWORD(pc);
                        PUSHWORD(u);
                        PUSHWORD(y);
                        PUSHWORD(x);
                        PUSHBYTE(dp.HighByte);
                        if ((md & MD_EM) != 0)
                        {
                            PUSHBYTE(w.LowByte);
                            PUSHBYTE(w.HighByte);
                            extra_cycles += 2;
                        }
                        PUSHBYTE(d.LowByte);
                        PUSHBYTE(d.HighByte);
                        PUSHBYTE(cc);
                        extra_cycles += 19;
                    }
                    else
                    {
                        cc &= (byte)~CC_E;
                        PUSHWORD(pc);
                        PUSHBYTE(cc);
                        extra_cycles += 10;
                    }
                }
                cc |= (byte)(CC_IF | CC_II);
                pc.LowWord = RM16(0xfff6);
                irq_callback(1);
            }
            else if (irq_state[0] != LineState.CLEAR_LINE && (cc & CC_II) == 0)
            {
                if ((int_state & 8) != 0)
                {
                    int_state &= unchecked((byte)~8);
                    extra_cycles += 7;
                }
                else
                {
                    cc |= CC_E;
                    PUSHWORD(pc);
                    PUSHWORD(u);
                    PUSHWORD(y);
                    PUSHWORD(x);
                    PUSHBYTE(dp.HighByte);
                    if ((md & MD_EM) != 0)
                    {
                        PUSHBYTE(w.LowByte);
                        PUSHBYTE(w.HighByte);
                        extra_cycles += 2;
                    }
                    PUSHBYTE(d.LowByte);
                    PUSHBYTE(d.HighByte);
                    PUSHBYTE(cc);
                    extra_cycles += 19;
                }
                cc |= CC_II;
                pc.LowWord = RM16(0xfff8);
                irq_callback(0);
            }
        }
        private void hd6309_reset()
        {
            int_state = 0;
            nmi_state = LineState.CLEAR_LINE;
            irq_state[0] = LineState.CLEAR_LINE;
            irq_state[0] = LineState.CLEAR_LINE;
            dp.d = 0;
            md = 0;
            cc |= CC_II;
            cc |= CC_IF;
            pc.LowWord = RM16(0xfffe);
            UpdateState();
        }
        public override void set_irq_line(int irqline, LineState state)
        {
            if (irqline == (int)LineState.INPUT_LINE_NMI)
            {
                if (nmi_state == state)
                {
                    return;
                }
                nmi_state = state;
                if (state == LineState.CLEAR_LINE)
                {
                    return;
                }
                if ((int_state & 32) == 0)
                {
                    return;
                }
                int_state &= unchecked((byte)~16);
                if ((int_state & 8) != 0)
                {
                    int_state &= unchecked((byte)(~8));
                    extra_cycles += 7;
                }
                else
                {
                    cc |= CC_E;
                    PUSHWORD(pc);
                    PUSHWORD(u);
                    PUSHWORD(y);
                    PUSHWORD(x);
                    PUSHBYTE(dp.HighByte);
                    if ((md & MD_EM) != 0)
                    {
                        PUSHBYTE(w.LowByte);
                        PUSHBYTE(w.HighByte);
                        extra_cycles += 2;
                    }

                    PUSHBYTE(d.LowByte);
                    PUSHBYTE(d.HighByte);
                    PUSHBYTE(cc);
                    extra_cycles += 19;
                }
                cc |= (byte)(CC_IF | CC_II);
                pc.LowWord = RM16(0xfffc);
            }
            else if (irqline < 2)
            {
                irq_state[irqline] = state;
                if (state == LineState.CLEAR_LINE)
                {
                    return;
                }
                CHECK_IRQ_LINES();
            }
        }
        public override void cpunum_set_input_line_and_vector(int cpunum, int line, LineState state, int vector)
        {
            Atime time1;
            time1 = Timer.get_current_time();
            foreach (irq irq1 in Cpuint.lirq)
            {
                if (irq1.cpunum == cpunum && irq1.line == line)
                {
                    if (Attotime.attotime_compare(irq1.time, time1) > 0)
                    {
                        irq1.time = time1;
                        break;
                    }
                    else
                    {
                        int i1 = 1;
                    }
                }
            }
            Timer.timer_set_internal(Cpuint.cpunum_empty_event_queue, "cpunum_empty_event_queue");
        }
        public override int ExecuteCycles(int cycles)
        {
            pendingCycles = cycles - extra_cycles;
            extra_cycles = 0;
            if ((int_state & (8 | 16)) != 0)
            {
                //debugger_instruction_hook(Machine, PCD);
                pendingCycles = 0;
            }
            else
            {
                do
                {
                    ppc = pc;
                    //debugger_instruction_hook(Machine, PCD);
                    ireg = ReadOp(pc.LowWord);
                    pc.LowWord++;
                    hd6309_main[ireg]();
                    pendingCycles -= cycle_counts_page0[ireg];
                }
                while (pendingCycles > 0);
                pendingCycles -= extra_cycles;
                extra_cycles = 0;
            }
            return cycles - pendingCycles;
        }
        private void fetch_effective_address()
        {
            byte postbyte = ReadOpArg(pc.LowWord);
            pc.LowWord++;
            switch (postbyte)
            {
                case 0x00: ea.LowWord = x.LowWord; break;
                case 0x01: ea.LowWord = (ushort)(x.LowWord + 1); break;
                case 0x02: ea.LowWord = (ushort)(x.LowWord + 2); break;
                case 0x03: ea.LowWord = (ushort)(x.LowWord + 3); break;
                case 0x04: ea.LowWord = (ushort)(x.LowWord + 4); break;
                case 0x05: ea.LowWord = (ushort)(x.LowWord + 5); break;
                case 0x06: ea.LowWord = (ushort)(x.LowWord + 6); break;
                case 0x07: ea.LowWord = (ushort)(x.LowWord + 7); break;
                case 0x08: ea.LowWord = (ushort)(x.LowWord + 8); break;
                case 0x09: ea.LowWord = (ushort)(x.LowWord + 9); break;
                case 0x0a: ea.LowWord = (ushort)(x.LowWord + 10); break;
                case 0x0b: ea.LowWord = (ushort)(x.LowWord + 11); break;
                case 0x0c: ea.LowWord = (ushort)(x.LowWord + 12); break;
                case 0x0d: ea.LowWord = (ushort)(x.LowWord + 13); break;
                case 0x0e: ea.LowWord = (ushort)(x.LowWord + 14); break;
                case 0x0f: ea.LowWord = (ushort)(x.LowWord + 15); break;

                case 0x10: ea.LowWord = (ushort)(x.LowWord - 16); break;
                case 0x11: ea.LowWord = (ushort)(x.LowWord - 15); break;
                case 0x12: ea.LowWord = (ushort)(x.LowWord - 14); break;
                case 0x13: ea.LowWord = (ushort)(x.LowWord - 13); break;
                case 0x14: ea.LowWord = (ushort)(x.LowWord - 12); break;
                case 0x15: ea.LowWord = (ushort)(x.LowWord - 11); break;
                case 0x16: ea.LowWord = (ushort)(x.LowWord - 10); break;
                case 0x17: ea.LowWord = (ushort)(x.LowWord - 9); break;
                case 0x18: ea.LowWord = (ushort)(x.LowWord - 8); break;
                case 0x19: ea.LowWord = (ushort)(x.LowWord - 7); break;
                case 0x1a: ea.LowWord = (ushort)(x.LowWord - 6); break;
                case 0x1b: ea.LowWord = (ushort)(x.LowWord - 5); break;
                case 0x1c: ea.LowWord = (ushort)(x.LowWord - 4); break;
                case 0x1d: ea.LowWord = (ushort)(x.LowWord - 3); break;
                case 0x1e: ea.LowWord = (ushort)(x.LowWord - 2); break;
                case 0x1f: ea.LowWord = (ushort)(x.LowWord - 1); break;

                case 0x20: ea.LowWord = y.LowWord; break;
                case 0x21: ea.LowWord = (ushort)(y.LowWord + 1); break;
                case 0x22: ea.LowWord = (ushort)(y.LowWord + 2); break;
                case 0x23: ea.LowWord = (ushort)(y.LowWord + 3); break;
                case 0x24: ea.LowWord = (ushort)(y.LowWord + 4); break;
                case 0x25: ea.LowWord = (ushort)(y.LowWord + 5); break;
                case 0x26: ea.LowWord = (ushort)(y.LowWord + 6); break;
                case 0x27: ea.LowWord = (ushort)(y.LowWord + 7); break;
                case 0x28: ea.LowWord = (ushort)(y.LowWord + 8); break;
                case 0x29: ea.LowWord = (ushort)(y.LowWord + 9); break;
                case 0x2a: ea.LowWord = (ushort)(y.LowWord + 10); break;
                case 0x2b: ea.LowWord = (ushort)(y.LowWord + 11); break;
                case 0x2c: ea.LowWord = (ushort)(y.LowWord + 12); break;
                case 0x2d: ea.LowWord = (ushort)(y.LowWord + 13); break;
                case 0x2e: ea.LowWord = (ushort)(y.LowWord + 14); break;
                case 0x2f: ea.LowWord = (ushort)(y.LowWord + 15); break;

                case 0x30: ea.LowWord = (ushort)(y.LowWord - 16); break;
                case 0x31: ea.LowWord = (ushort)(y.LowWord - 15); break;
                case 0x32: ea.LowWord = (ushort)(y.LowWord - 14); break;
                case 0x33: ea.LowWord = (ushort)(y.LowWord - 13); break;
                case 0x34: ea.LowWord = (ushort)(y.LowWord - 12); break;
                case 0x35: ea.LowWord = (ushort)(y.LowWord - 11); break;
                case 0x36: ea.LowWord = (ushort)(y.LowWord - 10); break;
                case 0x37: ea.LowWord = (ushort)(y.LowWord - 9); break;
                case 0x38: ea.LowWord = (ushort)(y.LowWord - 8); break;
                case 0x39: ea.LowWord = (ushort)(y.LowWord - 7); break;
                case 0x3a: ea.LowWord = (ushort)(y.LowWord - 6); break;
                case 0x3b: ea.LowWord = (ushort)(y.LowWord - 5); break;
                case 0x3c: ea.LowWord = (ushort)(y.LowWord - 4); break;
                case 0x3d: ea.LowWord = (ushort)(y.LowWord - 3); break;
                case 0x3e: ea.LowWord = (ushort)(y.LowWord - 2); break;
                case 0x3f: ea.LowWord = (ushort)(y.LowWord - 1); break;

                case 0x40: ea.LowWord = u.LowWord; break;
                case 0x41: ea.LowWord = (ushort)(u.LowWord + 1); break;
                case 0x42: ea.LowWord = (ushort)(u.LowWord + 2); break;
                case 0x43: ea.LowWord = (ushort)(u.LowWord + 3); break;
                case 0x44: ea.LowWord = (ushort)(u.LowWord + 4); break;
                case 0x45: ea.LowWord = (ushort)(u.LowWord + 5); break;
                case 0x46: ea.LowWord = (ushort)(u.LowWord + 6); break;
                case 0x47: ea.LowWord = (ushort)(u.LowWord + 7); break;
                case 0x48: ea.LowWord = (ushort)(u.LowWord + 8); break;
                case 0x49: ea.LowWord = (ushort)(u.LowWord + 9); break;
                case 0x4a: ea.LowWord = (ushort)(u.LowWord + 10); break;
                case 0x4b: ea.LowWord = (ushort)(u.LowWord + 11); break;
                case 0x4c: ea.LowWord = (ushort)(u.LowWord + 12); break;
                case 0x4d: ea.LowWord = (ushort)(u.LowWord + 13); break;
                case 0x4e: ea.LowWord = (ushort)(u.LowWord + 14); break;
                case 0x4f: ea.LowWord = (ushort)(u.LowWord + 15); break;

                case 0x50: ea.LowWord = (ushort)(u.LowWord - 16); break;
                case 0x51: ea.LowWord = (ushort)(u.LowWord - 15); break;
                case 0x52: ea.LowWord = (ushort)(u.LowWord - 14); break;
                case 0x53: ea.LowWord = (ushort)(u.LowWord - 13); break;
                case 0x54: ea.LowWord = (ushort)(u.LowWord - 12); break;
                case 0x55: ea.LowWord = (ushort)(u.LowWord - 11); break;
                case 0x56: ea.LowWord = (ushort)(u.LowWord - 10); break;
                case 0x57: ea.LowWord = (ushort)(u.LowWord - 9); break;
                case 0x58: ea.LowWord = (ushort)(u.LowWord - 8); break;
                case 0x59: ea.LowWord = (ushort)(u.LowWord - 7); break;
                case 0x5a: ea.LowWord = (ushort)(u.LowWord - 6); break;
                case 0x5b: ea.LowWord = (ushort)(u.LowWord - 5); break;
                case 0x5c: ea.LowWord = (ushort)(u.LowWord - 4); break;
                case 0x5d: ea.LowWord = (ushort)(u.LowWord - 3); break;
                case 0x5e: ea.LowWord = (ushort)(u.LowWord - 2); break;
                case 0x5f: ea.LowWord = (ushort)(u.LowWord - 1); break;

                case 0x60: ea.LowWord = s.LowWord; break;
                case 0x61: ea.LowWord = (ushort)(s.LowWord + 1); break;
                case 0x62: ea.LowWord = (ushort)(s.LowWord + 2); break;
                case 0x63: ea.LowWord = (ushort)(s.LowWord + 3); break;
                case 0x64: ea.LowWord = (ushort)(s.LowWord + 4); break;
                case 0x65: ea.LowWord = (ushort)(s.LowWord + 5); break;
                case 0x66: ea.LowWord = (ushort)(s.LowWord + 6); break;
                case 0x67: ea.LowWord = (ushort)(s.LowWord + 7); break;
                case 0x68: ea.LowWord = (ushort)(s.LowWord + 8); break;
                case 0x69: ea.LowWord = (ushort)(s.LowWord + 9); break;
                case 0x6a: ea.LowWord = (ushort)(s.LowWord + 10); break;
                case 0x6b: ea.LowWord = (ushort)(s.LowWord + 11); break;
                case 0x6c: ea.LowWord = (ushort)(s.LowWord + 12); break;
                case 0x6d: ea.LowWord = (ushort)(s.LowWord + 13); break;
                case 0x6e: ea.LowWord = (ushort)(s.LowWord + 14); break;
                case 0x6f: ea.LowWord = (ushort)(s.LowWord + 15); break;

                case 0x70: ea.LowWord = (ushort)(s.LowWord - 16); break;
                case 0x71: ea.LowWord = (ushort)(s.LowWord - 15); break;
                case 0x72: ea.LowWord = (ushort)(s.LowWord - 14); break;
                case 0x73: ea.LowWord = (ushort)(s.LowWord - 13); break;
                case 0x74: ea.LowWord = (ushort)(s.LowWord - 12); break;
                case 0x75: ea.LowWord = (ushort)(s.LowWord - 11); break;
                case 0x76: ea.LowWord = (ushort)(s.LowWord - 10); break;
                case 0x77: ea.LowWord = (ushort)(s.LowWord - 9); break;
                case 0x78: ea.LowWord = (ushort)(s.LowWord - 8); break;
                case 0x79: ea.LowWord = (ushort)(s.LowWord - 7); break;
                case 0x7a: ea.LowWord = (ushort)(s.LowWord - 6); break;
                case 0x7b: ea.LowWord = (ushort)(s.LowWord - 5); break;
                case 0x7c: ea.LowWord = (ushort)(s.LowWord - 4); break;
                case 0x7d: ea.LowWord = (ushort)(s.LowWord - 3); break;
                case 0x7e: ea.LowWord = (ushort)(s.LowWord - 2); break;
                case 0x7f: ea.LowWord = (ushort)(s.LowWord - 1); break;

                case 0x80: ea.LowWord = x.LowWord; x.LowWord++; break;
                case 0x81: ea.LowWord = x.LowWord; x.LowWord += 2; break;
                case 0x82: x.LowWord--; ea.LowWord = x.LowWord; break;
                case 0x83: x.LowWord -= 2; ea.LowWord = x.LowWord; break;
                case 0x84: ea.LowWord = x.LowWord; break;
                case 0x85: ea.LowWord = (ushort)(x.LowWord + SIGNED(d.LowByte)); break;
                case 0x86: ea.LowWord = (ushort)(x.LowWord + SIGNED(d.HighByte)); break;
                case 0x87: ea.LowWord = (ushort)(x.LowWord + SIGNED(w.HighByte)); break;
                case 0x88: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(x.LowWord + SIGNED(ea.LowByte)); break;
                case 0x89: ea = IMMWORD(); ea.LowWord += x.LowWord; break;
                case 0x8a: ea.LowWord = (ushort)(x.LowWord + SIGNED(w.LowByte)); break;
                case 0x8b: ea.LowWord = (ushort)(x.LowWord + d.LowWord); break;
                case 0x8c: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); break;
                case 0x8d: ea = IMMWORD(); ea.LowWord += pc.LowWord; break;
                case 0x8e: ea.LowWord = (ushort)(x.LowWord + w.LowWord); break;
                case 0x8f: ea.LowWord = w.LowWord; break;

                case 0x90: ea.LowWord = w.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0x91: ea.LowWord = x.LowWord; x.LowWord += 2; ea.LowWord = RM16(ea.LowWord); break;
                case 0x92: IIError(); break;
                case 0x93: x.LowWord -= 2; ea.LowWord = x.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0x94: ea.LowWord = x.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0x95: ea.LowWord = (ushort)(x.LowWord + SIGNED(d.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0x96: ea.LowWord = (ushort)(x.LowWord + SIGNED(d.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0x97: ea.LowWord = (ushort)(x.LowWord + SIGNED(w.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0x98: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(x.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0x99: ea = IMMWORD(); ea.LowWord += x.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0x9a: ea.LowWord = (ushort)(x.LowWord + SIGNED(w.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0x9b: ea.LowWord = (ushort)(x.LowWord + d.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0x9c: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0x9d: ea = IMMWORD(); ea.LowWord += pc.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0x9e: ea.LowWord = (ushort)(x.LowWord + w.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0x9f: ea = IMMWORD(); ea.LowWord = RM16(ea.LowWord); break;

                case 0xa0: ea.LowWord = y.LowWord; y.LowWord++; break;
                case 0xa1: ea.LowWord = y.LowWord; y.LowWord += 2; break;
                case 0xa2: y.LowWord--; ea.LowWord = y.LowWord; break;
                case 0xa3: y.LowWord -= 2; ea.LowWord = y.LowWord; break;
                case 0xa4: ea.LowWord = y.LowWord; break;
                case 0xa5: ea.LowWord = (ushort)(y.LowWord + SIGNED(d.LowByte)); break;
                case 0xa6: ea.LowWord = (ushort)(y.LowWord + SIGNED(d.HighByte)); break;
                case 0xa7: ea.LowWord = (ushort)(y.LowWord + SIGNED(w.HighByte)); break;
                case 0xa8: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(y.LowWord + SIGNED(ea.LowByte)); break;
                case 0xa9: ea = IMMWORD(); ea.LowWord += y.LowWord; break;
                case 0xaa: ea.LowWord = (ushort)(y.LowWord + SIGNED(w.LowByte)); break;
                case 0xab: ea.LowWord = (ushort)(y.LowWord + d.LowWord); break;
                case 0xac: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); break;
                case 0xad: ea = IMMWORD(); ea.LowWord += pc.LowWord; break;
                case 0xae: ea.LowWord = (ushort)(y.LowWord + w.LowWord); break;
                case 0xaf: ea = IMMWORD(); ea.LowWord += w.LowWord; break;

                case 0xb0: ea = IMMWORD(); ea.LowWord += w.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xb1: ea.LowWord = y.LowWord; y.LowWord += 2; ea.LowWord = RM16(ea.LowWord); break;
                case 0xb2: IIError(); break;
                case 0xb3: y.LowWord -= 2; ea.LowWord = y.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xb4: ea.LowWord = y.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xb5: ea.LowWord = (ushort)(y.LowWord + SIGNED(d.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xb6: ea.LowWord = (ushort)(y.LowWord + SIGNED(d.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xb7: ea.LowWord = (ushort)(y.LowWord + SIGNED(w.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xb8: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(y.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xb9: ea = IMMWORD(); ea.LowWord += y.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xba: ea.LowWord = (ushort)(y.LowWord + SIGNED(w.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xbb: ea.LowWord = (ushort)(y.LowWord + d.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0xbc: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xbd: ea = IMMWORD(); ea.LowWord += pc.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xbe: ea.LowWord = (ushort)(y.LowWord + w.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0xbf: IIError(); break;

                case 0xc0: ea.LowWord = u.LowWord; u.LowWord++; break;
                case 0xc1: ea.LowWord = u.LowWord; u.LowWord += 2; break;
                case 0xc2: u.LowWord--; ea.LowWord = u.LowWord; break;
                case 0xc3: u.LowWord -= 2; ea.LowWord = u.LowWord; break;
                case 0xc4: ea.LowWord = u.LowWord; break;
                case 0xc5: ea.LowWord = (ushort)(u.LowWord + SIGNED(d.LowByte)); break;
                case 0xc6: ea.LowWord = (ushort)(u.LowWord + SIGNED(d.HighByte)); break;
                case 0xc7: ea.LowWord = (ushort)(u.LowWord + SIGNED(w.HighByte)); break;
                case 0xc8: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(u.LowWord + SIGNED(ea.LowByte)); break;
                case 0xc9: ea = IMMWORD(); ea.LowWord += u.LowWord; break;
                case 0xca: ea.LowWord = (ushort)(u.LowWord + SIGNED(w.LowByte)); break;
                case 0xcb: ea.LowWord = (ushort)(u.LowWord + d.LowWord); break;
                case 0xcc: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); break;
                case 0xcd: ea = IMMWORD(); ea.LowWord += pc.LowWord; break;
                case 0xce: ea.LowWord = (ushort)(u.LowWord + w.LowWord); break;
                case 0xcf: ea.LowWord = w.LowWord; w.LowWord += 2; break;

                case 0xd0: ea.LowWord = w.LowWord; w.LowWord += 2; ea.LowWord = RM16(ea.LowWord); break;
                case 0xd1: ea.LowWord = u.LowWord; u.LowWord += 2; ea.LowWord = RM16(ea.LowWord); break;
                case 0xd2: IIError(); break;
                case 0xd3: u.LowWord -= 2; ea.LowWord = u.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xd4: ea.LowWord = u.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xd5: ea.LowWord = (ushort)(u.LowWord + SIGNED(d.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xd6: ea.LowWord = (ushort)(u.LowWord + SIGNED(d.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xd7: ea.LowWord = (ushort)(u.LowWord + SIGNED(w.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xd8: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(u.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xd9: ea = IMMWORD(); ea.LowWord += u.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xda: ea.LowWord = (ushort)(u.LowWord + SIGNED(w.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xdb: ea.LowWord = (ushort)(u.LowWord + d.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0xdc: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xdd: ea = IMMWORD(); ea.LowWord += pc.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xde: ea.LowWord = (ushort)(u.LowWord + w.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0xdf: IIError(); break;

                case 0xe0: ea.LowWord = s.LowWord; s.LowWord++; break;
                case 0xe1: ea.LowWord = s.LowWord; s.LowWord += 2; break;
                case 0xe2: s.LowWord--; ea.LowWord = s.LowWord; break;
                case 0xe3: s.LowWord -= 2; ea.LowWord = s.LowWord; break;
                case 0xe4: ea.LowWord = s.LowWord; break;
                case 0xe5: ea.LowWord = (ushort)(s.LowWord + SIGNED(d.LowByte)); break;
                case 0xe6: ea.LowWord = (ushort)(s.LowWord + SIGNED(d.HighByte)); break;
                case 0xe7: ea.LowWord = (ushort)(s.LowWord + SIGNED(w.HighByte)); break;
                case 0xe8: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(s.LowWord + SIGNED(ea.LowByte)); break;
                case 0xe9: ea = IMMWORD(); ea.LowWord += s.LowWord; break;
                case 0xea: ea.LowWord = (ushort)(s.LowWord + SIGNED(w.LowByte)); break;
                case 0xeb: ea.LowWord = (ushort)(s.LowWord + d.LowWord); break;
                case 0xec: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); break;
                case 0xed: ea = IMMWORD(); ea.LowWord += pc.LowWord; break;
                case 0xee: ea.LowWord = (ushort)(s.LowWord + w.LowWord); break;
                case 0xef: w.LowWord -= 2; ea.LowWord = w.LowWord; break;

                case 0xf0: w.LowWord -= 2; ea.LowWord = w.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xf1: ea.LowWord = s.LowWord; s.LowWord += 2; ea.LowWord = RM16(ea.LowWord); break;
                case 0xf2: IIError(); break;
                case 0xf3: s.LowWord -= 2; ea.LowWord = s.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xf4: ea.LowWord = s.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xf5: ea.LowWord = (ushort)(s.LowWord + SIGNED(d.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xf6: ea.LowWord = (ushort)(s.LowWord + SIGNED(d.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xf7: ea.LowWord = (ushort)(s.LowWord + SIGNED(w.HighByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xf8: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(s.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xf9: ea = IMMWORD(); ea.LowWord += s.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xfa: ea.LowWord = (ushort)(s.LowWord + SIGNED(w.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xfb: ea.LowWord = (ushort)(s.LowWord + d.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0xfc: ea.LowByte = IMMBYTE(); ea.LowWord = (ushort)(pc.LowWord + SIGNED(ea.LowByte)); ea.LowWord = RM16(ea.LowWord); break;
                case 0xfd: ea = IMMWORD(); ea.LowWord += pc.LowWord; ea.LowWord = RM16(ea.LowWord); break;
                case 0xfe: ea.LowWord = (ushort)(s.LowWord + w.LowWord); ea.LowWord = RM16(ea.LowWord); break;
                case 0xff: IIError(); break;
            }
            pendingCycles -= index_cycle[postbyte];
        }
        public override void SaveStateBinary(BinaryWriter writer)
        {
            writer.Write(ppc.LowWord);
            writer.Write(pc.LowWord);
            writer.Write(extra_cycles);
            writer.Write(d.LowWord);
            writer.Write(w.LowWord);
            writer.Write(dp.LowWord);
            writer.Write(u.LowWord);
            writer.Write(s.LowWord);
            writer.Write(x.LowWord);
            writer.Write(y.LowWord);
            writer.Write(v.LowWord);
            writer.Write(cc);
            writer.Write(md);
            writer.Write(ireg);
            writer.Write((byte)irq_state[0]);
            writer.Write((byte)irq_state[1]);
            writer.Write(int_state);
            writer.Write((byte)nmi_state);
            writer.Write(ea.LowWord);
            writer.Write(z8);
            writer.Write(dummy_byte);
            writer.Write(z16);
            writer.Write(TotalExecutedCycles);
            writer.Write(PendingCycles);
        }
        public override void LoadStateBinary(BinaryReader reader)
        {
            ppc.LowWord = reader.ReadUInt16();
            pc.LowWord = reader.ReadUInt16();
            extra_cycles = reader.ReadInt32();
            d.LowWord = reader.ReadUInt16();
            w.LowWord = reader.ReadUInt16();
            dp.LowWord = reader.ReadUInt16();
            u.LowWord = reader.ReadUInt16();
            s.LowWord = reader.ReadUInt16();
            x.LowWord = reader.ReadUInt16();
            y.LowWord = reader.ReadUInt16();
            v.LowWord = reader.ReadUInt16();
            cc = reader.ReadByte();
            md = reader.ReadByte();
            ireg = reader.ReadByte();
            irq_state[0] = (LineState)reader.ReadByte();
            irq_state[1] = (LineState)reader.ReadByte();
            int_state = reader.ReadByte();
            nmi_state = (LineState)reader.ReadByte();
            ea.LowWord = reader.ReadUInt16();
            z8 = reader.ReadByte();
            dummy_byte = reader.ReadByte();
            z16 = reader.ReadUInt16();
            TotalExecutedCycles = reader.ReadUInt64();
            PendingCycles = reader.ReadInt32();
        }
    }
}
