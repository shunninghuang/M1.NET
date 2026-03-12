using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mame;

namespace cpu.hd6309
{
    public unsafe partial class Hd6309
    {
        void illegal()
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
                pendingCycles -= 2;
            }
            PUSHBYTE(d.LowByte);
            PUSHBYTE(d.HighByte);
            PUSHBYTE(cc);
            pc.LowWord = RM16(0xfff0);
        }
        void IIError()
        {
            SEII();
            illegal();
        }
        void DZError()
        {
            SEDZ();
            illegal();
        }
        void neg_di()
        {
            ushort r, t;
            t = DIRBYTE();
            r = (ushort)(-t);
            CLR_NZVC();
            SET_FLAGS8(0, t, r);
            WM(ea.LowWord, (byte)r);
        }
        void oim_di()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = DIRBYTE();
            r = (byte)(im | t);
            CLR_NZV();
            SET_NZ8(r);
            WM(ea.LowWord, r);
        }
        void aim_di()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = DIRBYTE();
            r = (byte)(im & t);
            CLR_NZV();
            SET_NZ8(r);
            WM(ea.LowWord, r);
        }
        void com_di()
        {
            byte t;
            t = DIRBYTE();
            t = (byte)(~t);
            CLR_NZV();
            SET_NZ8(t);
            SEC();
            WM(ea.LowWord, t);
        }
        void lsr_di()
        {
            byte t;
            t = DIRBYTE();
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            t >>= 1;
            SET_Z8(t);
            WM(ea.LowWord, t);
        }
        void eim_di()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = DIRBYTE();
            r = (byte)(im ^ t);
            CLR_NZV();
            SET_NZ8(r);
            WM(ea.LowWord, r);
        }
        void ror_di()
        {
            byte t, r;
            t = DIRBYTE();
            r = (byte)((cc & CC_C) << 7);
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            r |= (byte)(t >> 1);
            SET_NZ8(r);
            WM(ea.LowWord, r);
        }
        void asr_di()
        {
            byte t;
            t = DIRBYTE();
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            t = (byte)((t & 0x80) | (t >> 1));
            SET_NZ8(t);
            WM(ea.LowWord, t);
        }
        void asl_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            WM(ea.LowWord, (byte)r);
        }
        void rol_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)((cc & CC_C) | (t << 1));
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            WM(ea.LowWord, (byte)r);
        }
        void dec_di()
        {
            byte t;
            t = DIRBYTE();
            --t;
            CLR_NZV();
            SET_FLAGS8D(t);
            WM(ea.LowWord, t);
        }
        void tim_di()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = DIRBYTE();
            r = (byte)(im & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void inc_di()
        {
            byte t;
            t = DIRBYTE();
            ++t;
            CLR_NZV();
            SET_FLAGS8I(t);
            WM(ea.LowWord, t);
        }
        void tst_di()
        {
            byte t;
            t = DIRBYTE();
            CLR_NZV();
            SET_NZ8(t);
        }
        void jmp_di()
        {
            DIRECT();
            pc.d = ea.d;
        }
        void clr_di()
        {
            uint dummy;
            DIRECT();
            dummy = RM(ea.LowWord);
            WM(ea.LowWord, 0);
            CLR_NZVC();
            SEZ();
        }
        void nop()
        {

        }
        void sync()
        {
            int_state |= 16;
            CHECK_IRQ_LINES();
            if ((int_state & 16) != 0)
            {
                if (pendingCycles > 0)
                {
                    pendingCycles = 0;
                }
            }
        }
        void sexw()
        {
            Register q = new Register();
            q.d = SIGNED_16(w.LowWord);
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZ();
            SET_N16(d.LowWord);
            SET_Z(q.d);
        }
        void lbra()
        {
            ea = IMMWORD();
            pc.LowWord += ea.LowWord;
            if (ea.LowWord == 0xfffd)
            {
                if (pendingCycles > 0)
                {
                    pendingCycles = 0;
                }
            }
        }
        void lbsr()
        {
            ea = IMMWORD();
            PUSHWORD(pc);
            pc.LowWord += ea.LowWord;
        }
        void daa()
        {
            byte msn, lsn;
            ushort t, cf = 0;
            msn = (byte)(d.HighByte & 0xf0);
            lsn = (byte)(d.HighByte & 0x0f);
            if (lsn > 0x09 || (cc & CC_H) != 0)
            {
                cf |= 0x06;
            }
            if (msn > 0x80 && lsn > 0x09)
            {
                cf |= 0x60;
            }
            if (msn > 0x90 || (cc & CC_C) != 0)
            {
                cf |= 0x60;
            }
            t = (ushort)(cf + d.HighByte);
            CLR_NZV();
            SET_NZ8((byte)t);
            SET_C8(t);
            d.HighByte = (byte)t;
        }
        void orcc()
        {
            byte t;
            t = IMMBYTE();
            cc |= t;
            CHECK_IRQ_LINES();
        }
        void andcc()
        {
            byte t;
            t = IMMBYTE();
            cc &= t;
            CHECK_IRQ_LINES();
        }
        void sex()
        {
            ushort t;
            t = SIGNED(d.LowByte);
            d.LowWord = t;
            CLR_NZ();
            SET_NZ16(t);
        }
        void exg()
        {
            ushort t1, t2;
            byte tb;
            int promote = 0;
            tb = IMMBYTE();
            if (((tb ^ (tb >> 4)) & 0x08) != 0)
            {
                promote = 1;
            }
            switch (tb >> 4)
            {
                case 0: t1 = d.LowWord; break;
                case 1: t1 = x.LowWord; break;
                case 2: t1 = y.LowWord; break;
                case 3: t1 = u.LowWord; break;
                case 4: t1 = s.LowWord; break;
                case 5: t1 = pc.LowWord; break;
                case 6: t1 = w.LowWord; break;
                case 7: t1 = v.LowWord; break;
                case 8: t1 = (ushort)(promote != 0 ? d.HighByte + ((d.HighByte) << 8) : d.HighByte); break;
                case 9: t1 = (ushort)(promote != 0 ? d.LowByte + ((d.LowByte) << 8) : d.LowByte); break;
                case 10: t1 = (ushort)(promote != 0 ? cc + ((cc) << 8) : cc); break;
                case 11: t1 = (ushort)(promote != 0 ? dp.HighByte + ((dp.HighByte) << 8) : dp.HighByte); break;
                case 12: t1 = 0; break;
                case 13: t1 = 0; break;
                case 14: t1 = (ushort)(promote != 0 ? w.HighByte + ((w.HighByte) << 8) : w.HighByte); break;
                default: t1 = (ushort)(promote != 0 ? w.LowByte + ((w.LowByte) << 8) : w.LowByte); break;
            }
            switch (tb & 15)
            {
                case 0: t2 = d.LowWord; break;
                case 1: t2 = x.LowWord; break;
                case 2: t2 = y.LowWord; break;
                case 3: t2 = u.LowWord; break;
                case 4: t2 = s.LowWord; break;
                case 5: t2 = pc.LowWord; break;
                case 6: t2 = w.LowWord; break;
                case 7: t2 = v.LowWord; break;
                case 8: t2 = (ushort)(promote != 0 ? d.HighByte + ((d.HighByte) << 8) : d.HighByte); break;
                case 9: t2 = (ushort)(promote != 0 ? d.LowByte + ((d.LowByte) << 8) : d.LowByte); break;
                case 10: t2 = (ushort)(promote != 0 ? cc + ((cc) << 8) : cc); break;
                case 11: t2 = (ushort)(promote != 0 ? dp.HighByte + ((dp.HighByte) << 8) : dp.HighByte); break;
                case 12: t2 = 0; break;
                case 13: t2 = 0; break;
                case 14: t2 = (ushort)(promote != 0 ? w.HighByte + ((w.HighByte) << 8) : w.HighByte); break;
                default: t2 = (ushort)(promote != 0 ? w.LowByte + ((w.LowByte) << 8) : w.LowByte); break;
            }

            switch (tb >> 4)
            {
                case 0: d.LowWord = t2; break;
                case 1: x.LowWord = t2; break;
                case 2: y.LowWord = t2; break;
                case 3: u.LowWord = t2; break;
                case 4: s.LowWord = t2; break;
                case 5: pc.LowWord = t2; break;
                case 6: w.LowWord = t2; break;
                case 7: v.LowWord = t2; break;
                case 8: d.HighByte = (byte)(promote != 0 ? t2 >> 8 : t2); break;
                case 9: d.LowByte = (byte)(promote != 0 ? t2 & 0xff : t2); break;
                case 10: cc = (byte)(promote != 0 ? t2 & 0xff : t2); break;
                case 11: dp.HighByte = (byte)(promote != 0 ? t2 >> 8 : t2); break;
                case 12: /* 0 = t2 */ break;
                case 13: /* 0 = t2 */ break;
                case 14: w.HighByte = (byte)(promote != 0 ? t2 >> 8 : t2); break;
                case 15: w.LowByte = (byte)(promote != 0 ? t2 & 0xff : t2); break;
            }
            switch (tb & 15)
            {
                case 0: d.LowWord = t1; break;
                case 1: x.LowWord = t1; break;
                case 2: y.LowWord = t1; break;
                case 3: u.LowWord = t1; break;
                case 4: s.LowWord = t1; break;
                case 5: pc.LowWord = t1; break;
                case 6: w.LowWord = t1; break;
                case 7: v.LowWord = t1; break;
                case 8: d.HighByte = (byte)(promote != 0 ? t1 >> 8 : t1); break;
                case 9: d.LowByte = (byte)(promote != 0 ? t1 & 0xff : t1); break;
                case 10: cc = (byte)(promote != 0 ? t1 & 0xff : t1); break;
                case 11: dp.HighByte = (byte)(promote != 0 ? t1 >> 8 : t1); break;
                case 12: /* 0 = t1 */ break;
                case 13: /* 0 = t1 */ break;
                case 14: w.HighByte = (byte)(promote != 0 ? t1 >> 8 : t1); break;
                case 15: w.LowByte = (byte)(promote != 0 ? t1 & 0xff : t1); break;
            }
        }
        void tfr()
        {
            byte tb;
            ushort t;
            int promote = 0;
            tb = IMMBYTE();
            if (((tb ^ (tb >> 4)) & 0x08) != 0)
            {
                promote = 1;
            }
            switch (tb >> 4)
            {
                case 0: t = d.LowWord; break;
                case 1: t = x.LowWord; break;
                case 2: t = y.LowWord; break;
                case 3: t = u.LowWord; break;
                case 4: t = s.LowWord; break;
                case 5: t = pc.LowWord; break;
                case 6: t = w.LowWord; break;
                case 7: t = v.LowWord; break;
                case 8: t = (ushort)(promote != 0 ? d.HighByte + ((d.HighByte) << 8) : d.HighByte); break;
                case 9: t = (ushort)(promote != 0 ? d.LowByte + ((d.LowByte) << 8) : d.LowByte); break;
                case 10: t = (ushort)(promote != 0 ? cc + ((cc) << 8) : cc); break;
                case 11: t = (ushort)(promote != 0 ? dp.HighByte + ((dp.HighByte) << 8) : dp.HighByte); break;
                case 12: t = 0; break;
                case 13: t = 0; break;
                case 14: t = (ushort)(promote != 0 ? w.HighByte + ((w.HighByte) << 8) : w.HighByte); break;
                default: t = (ushort)(promote != 0 ? w.LowByte + ((w.LowByte) << 8) : w.LowByte); break;
            }

            switch (tb & 15)
            {
                case 0: d.LowWord = t; break;
                case 1: x.LowWord = t; break;
                case 2: y.LowWord = t; break;
                case 3: u.LowWord = t; break;
                case 4: s.LowWord = t; break;
                case 5: pc.LowWord = t; break;
                case 6: w.LowWord = t; break;
                case 7: v.LowWord = t; break;
                case 8: d.HighByte = (byte)(promote != 0 ? t >> 8 : t); break;
                case 9: d.LowByte = (byte)(promote != 0 ? t & 0xff : t); break;
                case 10: cc = (byte)(promote != 0 ? t & 0xff : t); break;
                case 11: dp.HighByte = (byte)(promote != 0 ? t >> 8 : t); break;
                case 12: /* 0 = t */ break;
                case 13: /* 0 = t */ break;
                case 14: w.HighByte = (byte)(promote != 0 ? t >> 8 : t); break;
                case 15: w.LowByte = (byte)(promote != 0 ? t & 0xff : t); break;
            }
        }
        void bra()
        {
            byte t;
            t = IMMBYTE();
            pc.LowWord += SIGNED(t);
            if (t == 0xfe)
            {
                if (pendingCycles > 0)
                {
                    pendingCycles = 0;
                }
            }
        }
        void brn()
        {
            byte t;
            t = IMMBYTE();
        }
        void lbrn()
        {
            ea = IMMWORD();
        }
        void bhi()
        {
            BRANCH((cc & (CC_Z | CC_C)) == 0);
        }
        void lbhi()
        {
            LBRANCH((cc & (CC_Z | CC_C)) == 0);
        }
        void bls()
        {
            BRANCH((cc & (CC_Z | CC_C)) != 0);
        }
        void lbls()
        {
            LBRANCH((cc & (CC_Z | CC_C)) != 0);
        }
        void bcc()
        {
            BRANCH((cc & CC_C) == 0);
        }
        void lbcc()
        {
            LBRANCH((cc & CC_C) == 0);
        }
        void bcs()
        {
            BRANCH((cc & CC_C) != 0);
        }
        void lbcs()
        {
            LBRANCH((cc & CC_C) != 0);
        }
        void bne()
        {
            BRANCH((cc & CC_Z) == 0);
        }
        void lbne()
        {
            LBRANCH((cc & CC_Z) == 0);
        }
        void beq()
        {
            BRANCH((cc & CC_Z) != 0);
        }
        void lbeq()
        {
            LBRANCH((cc & CC_Z) != 0);
        }
        void bvc()
        {
            BRANCH((cc & CC_V) == 0);
        }
        void lbvc()
        {
            LBRANCH((cc & CC_V) == 0);
        }
        void bvs()
        {
            BRANCH((cc & CC_V) != 0);
        }
        void lbvs()
        {
            LBRANCH((cc & CC_V) != 0);
        }
        void bpl()
        {
            BRANCH((cc & CC_N) == 0);
        }
        void lbpl()
        {
            LBRANCH((cc & CC_N) == 0);
        }
        void bmi()
        {
            BRANCH((cc & CC_N) != 0);
        }
        void lbmi()
        {
            LBRANCH((cc & CC_N) != 0);
        }
        void bge()
        {
            BRANCH(NXORV() == 0);
        }
        void lbge()
        {
            LBRANCH(NXORV() == 0);
        }
        void blt()
        {
            BRANCH(NXORV() != 0);
        }
        void lblt()
        {
            LBRANCH(NXORV() != 0);
        }
        void bgt()
        {
            BRANCH((NXORV() != 0 || ((cc & CC_Z) != 0)) == false);
        }
        void lbgt()
        {
            LBRANCH((NXORV() != 0 || ((cc & CC_Z) != 0)) == false);
        }
        void ble()
        {
            BRANCH((NXORV() != 0 || ((cc & CC_Z) != 0)));
        }
        void lble()
        {
            LBRANCH((NXORV() != 0 || ((cc & CC_Z) != 0)));
        }
        public unsafe void REGREG_PREAMBLE(ref bool promote, ref bool large, ref int isrc8, ref int isrc16, ref int idst8, ref int idst16)
        {
            byte tb = IMMBYTE();
            if (((tb ^ (tb >> 4)) & 0x08) != 0)
            {
                promote = true;
            }
            switch (tb >> 4)
            {
                case 0:
                    isrc16 = 1;
                    //src16Reg = (ushort*)(&d);
                    large = true;
                    break;
                case 1:
                    isrc16 = 2;
                    //src16Reg = &X;
                    large = true;
                    break;
                case 2:
                    isrc16 = 3;
                    //src16Reg = &Y;
                    large = true;
                    break;
                case 3:
                    isrc16 = 4;
                    //src16Reg = &U;
                    large = true;
                    break;
                case 4:
                    isrc16 = 5;
                    //src16Reg = &S;
                    large = true;
                    break;
                case 5:
                    isrc16 = 6;
                    //src16Reg = &PC;
                    large = true;
                    break;
                case 6:
                    isrc16 = 7;
                    //src16Reg = &W;
                    large = true;
                    break;
                case 7:
                    isrc16 = 8;
                    //src16Reg = &V;
                    large = true;
                    break;
                case 8:
                    if (promote)
                    {
                        isrc16 = 1;
                        //src16Reg = &D;
                    }
                    else
                    {
                        isrc8 = 1;
                        //src8Reg = &A;
                    }
                    break;
                case 9:
                    if (promote)
                    {
                        isrc16 = 1;
                        //src16Reg = &D;
                    }
                    else
                    {
                        isrc8 = 2;
                        //src8Reg = &B;
                    }
                    break;
                case 10:
                    if (promote)
                    {
                        isrc16 = 9;
                        //src16Reg = &z16;
                    }
                    else
                    {
                        isrc8 = 3;
                        //src8Reg = &CC;
                    }
                    break;
                case 11:
                    if (promote)
                    {
                        isrc16 = 9;
                        //src16Reg = &z16;
                    }
                    else
                    {
                        isrc8 = 4;
                        //src8Reg = &DP;
                    }
                    break;
                case 12:
                    if (promote)
                    {
                        isrc16 = 9;
                        //src16Reg = &z16;
                    }
                    else
                    {
                        isrc8 = 5;
                        //src8Reg = &z8;
                    }
                    break;
                case 13:
                    if (promote)
                    {
                        isrc16 = 9;
                        //src16Reg = &z16;
                    }
                    else
                    {
                        isrc8 = 5;
                        //src8Reg = &z8;
                    }
                    break;
                case 14:
                    if (promote)
                    {
                        isrc16 = 7;
                        //src16Reg = &W;
                    }
                    else
                    {
                        isrc8 = 6;
                        //src8Reg = &E;
                    }
                    break;
                default:
                    if (promote)
                    {
                        isrc16 = 7;
                        //src16Reg = &W;
                    }
                    else
                    {
                        isrc8 = 7;
                        //src8Reg = &F;
                    }
                    break;
            }
            switch (tb & 15)
            {
                case 0:
                    idst16 = 1;
                    //dst16Reg = &D;
                    large = true;
                    break;
                case 1:
                    idst16 = 2;
                    //dst16Reg = &X;
                    large = true;
                    break;
                case 2:
                    idst16 = 3;
                    //dst16Reg = &Y;
                    large = true;
                    break;
                case 3:
                    idst16 = 4;
                    //dst16Reg = &U;
                    large = true;
                    break;
                case 4:
                    idst16 = 5;
                    //dst16Reg = &S;
                    large = true;
                    break;
                case 5:
                    idst16 = 6;
                    //dst16Reg = &PC;
                    large = true;
                    break;
                case 6:
                    idst16 = 7;
                    //dst16Reg = &W;
                    large = true;
                    break;
                case 7:
                    idst16 = 8;
                    //dst16Reg = &V;
                    large = true;
                    break;
                case 8:
                    if (promote)
                    {
                        idst16 = 1;
                        //dst16Reg = &D;
                    }
                    else
                    {
                        idst8 = 1;
                        //dst8Reg = &A;
                    }
                    break;
                case 9:
                    if (promote)
                    {
                        idst16 = 1;
                        //dst16Reg = &D;
                    }
                    else
                    {
                        idst8 = 2;
                        //dst8Reg = &B;
                    }
                    break;
                case 10:
                    if (promote)
                    {
                        idst16 = 9;
                        //dst16Reg = &z16;
                    }
                    else
                    {
                        idst8 = 3;
                        //dst8Reg = &CC;
                    }
                    break;
                case 11:
                    if (promote)
                    {
                        idst16 = 9;
                        //dst16Reg = &z16;
                    }
                    else
                    {
                        idst8 = 4;
                        //dst8Reg = &DP;
                    }
                    break;
                case 12:
                    if (promote)
                    {
                        idst16 = 9;
                        //dst16Reg = &z16;
                    }
                    else
                    {
                        idst8 = 5;
                        //dst8Reg = &z8;
                    }
                    break;
                case 13:
                    if (promote)
                    {
                        idst16 = 9;
                        //dst16Reg = &z16;
                    }
                    else
                    {
                        idst8 = 5;
                        //dst8Reg = &z8;
                    }
                    break;
                case 14:
                    if (promote)
                    {
                        idst16 = 7;
                        //dst16Reg = &W;
                    }
                    else
                    {
                        idst8 = 6;
                        //dst8Reg = &E;
                    }
                    break;
                default:
                    if (promote)
                    {
                        idst16 = 7;
                        //dst16Reg = &W;
                    }
                    else
                    {
                        idst8 = 7;
                        //dst8Reg = &F;
                    }
                    break;
            }
        }
        public byte getsrc8(int isrc8)
        {
            byte b1 = 0;
            if (isrc8 == 1)
            {
                b1 = d.HighByte;
            }
            else if (isrc8 == 2)
            {
                b1 = d.LowByte;
            }
            else if (isrc8 == 3)
            {
                b1 = cc;
            }
            else if (isrc8 == 4)
            {
                b1 = dp.HighByte;
            }
            else if (isrc8 == 5)
            {
                b1 = z8;
            }
            else if (isrc8 == 6)
            {
                b1 = w.HighByte;
            }
            else if (isrc8 == 7)
            {
                b1 = w.LowByte;
            }
            return b1;
        }
        public ushort getsrc16(int isrc16)
        {
            ushort u1 = 0;
            if (isrc16 == 1)
            {
                u1 = d.LowWord;
            }
            else if (isrc16 == 2)
            {
                u1 = x.LowWord;
            }
            else if (isrc16 == 3)
            {
                u1 = y.LowWord;
            }
            else if (isrc16 == 4)
            {
                u1 = u.LowWord;
            }
            else if (isrc16 == 5)
            {
                u1 = s.LowWord;
            }
            else if (isrc16 == 6)
            {
                u1 = pc.LowWord;
            }
            else if (isrc16 == 7)
            {
                u1 = w.LowWord;
            }
            else if (isrc16 == 8)
            {
                u1 = v.LowWord;
            }
            else if (isrc16 == 9)
            {
                u1 = z16;
            }
            return u1;
        }
        public void setdst8(int idst8, byte b1)
        {
            if (idst8 == 1)
            {
                d.HighByte = b1;
            }
            else if (idst8 == 2)
            {
                d.LowByte = b1;
            }
            else if (idst8 == 3)
            {
                cc = b1;
            }
            else if (idst8 == 4)
            {
                dp.HighByte = b1;
            }
            else if (idst8 == 5)
            {
                z8 = b1;
            }
            else if (idst8 == 6)
            {
                w.HighByte = b1;
            }
            else if (idst8 == 7)
            {
                w.LowByte = b1;
            }
        }
        public void setdst16(int idst16, ushort u1)
        {
            if (idst16 == 1)
            {
                d.LowWord = u1;
            }
            else if (idst16 == 2)
            {
                x.LowWord = u1;
            }
            else if (idst16 == 3)
            {
                y.LowWord = u1;
            }
            else if (idst16 == 4)
            {
                u.LowWord = u1;
            }
            else if (idst16 == 5)
            {
                s.LowWord = u1;
            }
            else if (idst16 == 6)
            {
                pc.LowWord = u1;
            }
            else if (idst16 == 7)
            {
                w.LowWord = u1;
            }
            else if (idst16 == 8)
            {
                v.LowWord = u1;
            }
            else if (idst16 == 9)
            {
                z16 = u1;
            }
        }
        void addr_r()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(isrc16) + getsrc16(idst16));
                CLR_NZVC();
                setdst16(idst16, (ushort)r16);
                SET_FLAGS16(getsrc16(isrc16), getsrc16(idst16), r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(isrc8) + getsrc8(idst8));
                CLR_NZVC();
                setdst8(idst8, (byte)r8);
                SET_FLAGS8(getsrc8(isrc8), getsrc8(idst8), r8);
            }
        }
        void adcr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(isrc16) + getsrc16(idst16) + (cc & CC_C));
                CLR_NZVC();
                setdst16(idst16, (ushort)r16);
                SET_FLAGS16(getsrc16(isrc16), getsrc16(idst16), r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(isrc8) + getsrc8(idst8) + (cc & CC_C));
                CLR_NZVC();
                setdst8(idst8, (byte)r8);
                SET_FLAGS8(getsrc8(isrc8), getsrc8(idst8), r8);
            }
        }
        void subr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(idst16) - getsrc16(isrc16));
                CLR_NZVC();
                setdst16(idst16, (ushort)r16);
                SET_FLAGS16(getsrc16(idst16), getsrc16(isrc16), r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(idst8) - getsrc8(isrc8));
                CLR_NZVC();
                setdst8(idst8, (byte)r8);
                SET_FLAGS8(getsrc8(idst8), getsrc8(isrc8), r8);
            }
        }
        void sbcr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(idst16) - getsrc16(isrc16) - (cc & CC_C));
                CLR_NZVC();
                setdst16(idst16, (ushort)r16);
                SET_FLAGS16(getsrc16(idst16), getsrc16(isrc16), r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(idst8) - getsrc8(isrc8) - (cc & CC_C));
                CLR_NZVC();
                setdst8(idst8, (byte)r8);
                SET_FLAGS8(getsrc8(idst8), getsrc8(isrc8), r8);
            }
        }
        void andr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(isrc16) & getsrc16(idst16));
                CLR_NZV();
                setdst16(idst16, (ushort)r16);
                SET_NZ16((ushort)r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(isrc8) & getsrc8(idst8));
                CLR_NZV();
                setdst8(idst8, (byte)r8);
                SET_NZ8((byte)r8);
            }
        }
        void orr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(isrc16) | getsrc16(idst16));
                CLR_NZV();
                setdst16(idst16, (ushort)r16);
                SET_NZ16((ushort)r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(isrc8) | getsrc8(idst8));
                CLR_NZV();
                setdst8(idst8, (byte)r8);
                SET_NZ8((byte)r8);
            }
        }
        void eorr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)(getsrc16(isrc16) ^ getsrc16(idst16));
                CLR_NZV();
                setdst16(idst16, (ushort)r16);
                SET_NZ16((ushort)r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(isrc8) ^ getsrc8(idst8));
                CLR_NZV();
                setdst8(idst8, (byte)r8);
                SET_NZ8((byte)r8);
            }
        }
        void cmpr()
        {
            ushort r8;
            uint r16;
            int isrc8 = 0, isrc16 = 0, idst8 = 0, idst16 = 0;
            z8 = 0;
            z16 = 0;
            bool promote = false, large = false;
            REGREG_PREAMBLE(ref promote, ref large, ref isrc8, ref isrc16, ref idst8, ref idst16);
            if (large)
            {
                r16 = (uint)getsrc16(idst16) - (uint)getsrc16(isrc16);
                CLR_NZVC();
                SET_FLAGS16(getsrc16(idst16), getsrc16(isrc16), r16);
            }
            else
            {
                r8 = (ushort)(getsrc8(idst8) - getsrc8(isrc8));
                CLR_NZVC();
                SET_FLAGS8(getsrc8(idst8), getsrc8(isrc8), r8);
            }
        }
        void tfmpp()
        {
            byte tb, srcValue = 0;
            tb = IMMBYTE();
            if (w.LowWord != 0)
            {
                switch (tb >> 4)
                {
                    case 0: srcValue = RM(d.LowWord++); break;
                    case 1: srcValue = RM(x.LowWord++); break;
                    case 2: srcValue = RM(y.LowWord++); break;
                    case 3: srcValue = RM(u.LowWord++); break;
                    case 4: srcValue = RM(s.LowWord++); break;
                    default: IIError(); return; break;
                }
                switch (tb & 15)
                {
                    case 0: WM(d.LowWord++, srcValue); break;
                    case 1: WM(x.LowWord++, srcValue); break;
                    case 2: WM(y.LowWord++, srcValue); break;
                    case 3: WM(u.LowWord++, srcValue); break;
                    case 4: WM(s.LowWord++, srcValue); break;
                    default: IIError(); return; break;
                }
                pc.d = pc.d - 3;
                w.LowWord--;
            }
            else
            {
                pendingCycles -= 6;
            }
        }
        void tfmmm()
        {
            byte tb, srcValue = 0;
            tb = IMMBYTE();
            if (w.LowWord != 0)
            {
                switch (tb >> 4)
                {
                    case 0: srcValue = RM(d.LowWord--); break;
                    case 1: srcValue = RM(x.LowWord--); break;
                    case 2: srcValue = RM(y.LowWord--); break;
                    case 3: srcValue = RM(u.LowWord--); break;
                    case 4: srcValue = RM(s.LowWord--); break;
                    default: IIError(); return; break;
                }
                switch (tb & 15)
                {
                    case 0: WM(d.LowWord--, srcValue); break;
                    case 1: WM(x.LowWord--, srcValue); break;
                    case 2: WM(y.LowWord--, srcValue); break;
                    case 3: WM(u.LowWord--, srcValue); break;
                    case 4: WM(s.LowWord--, srcValue); break;
                    default: IIError(); return; break;
                }
                pc.d = pc.d - 3;
                w.LowWord--;
            }
            else
            {
                pendingCycles -= 6;
            }
        }
        void tfmpc()
        {
            byte tb, srcValue = 0;
            tb = IMMBYTE();
            if (w.LowWord != 0)
            {
                switch (tb >> 4)
                {
                    case 0: srcValue = RM(d.LowWord++); break;
                    case 1: srcValue = RM(x.LowWord++); break;
                    case 2: srcValue = RM(y.LowWord++); break;
                    case 3: srcValue = RM(u.LowWord++); break;
                    case 4: srcValue = RM(s.LowWord++); break;
                    default: IIError(); return; break;		/* reg PC thru F */
                }
                switch (tb & 15)
                {
                    case 0: WM(d.LowWord, srcValue); break;
                    case 1: WM(x.LowWord, srcValue); break;
                    case 2: WM(y.LowWord, srcValue); break;
                    case 3: WM(u.LowWord, srcValue); break;
                    case 4: WM(s.LowWord, srcValue); break;
                    default: IIError(); return; break;		/* reg PC thru F */
                }
                pc.d = pc.d - 3;
                w.LowWord--;
            }
            else
            {
                pendingCycles -= 6;
            }
        }
        void tfmcp()
        {
            byte tb, srcValue = 0;
            tb = IMMBYTE();
            if (w.LowWord != 0)
            {
                switch (tb >> 4)
                {
                    case 0: srcValue = RM(d.LowWord); break;
                    case 1: srcValue = RM(x.LowWord); break;
                    case 2: srcValue = RM(y.LowWord); break;
                    case 3: srcValue = RM(u.LowWord); break;
                    case 4: srcValue = RM(s.LowWord); break;
                    default: IIError(); return; break;		/* reg PC thru F */
                }
                switch (tb & 15)
                {
                    case 0: WM(d.LowWord++, srcValue); break;
                    case 1: WM(x.LowWord++, srcValue); break;
                    case 2: WM(y.LowWord++, srcValue); break;
                    case 3: WM(u.LowWord++, srcValue); break;
                    case 4: WM(s.LowWord++, srcValue); break;
                    default: IIError(); return; break;		/* reg PC thru F */
                }
                pc.d = pc.d - 3;
                w.LowWord--;
            }
            else
            {
                pendingCycles -= 6;
            }
        }
        void leax()
        {
            fetch_effective_address();
            x.LowWord = ea.LowWord;
            CLR_Z();
            SET_Z(x.LowWord);
        }

        void leay()
        {
            fetch_effective_address();
            y.LowWord = ea.LowWord;
            CLR_Z();
            SET_Z(y.LowWord);
        }
        void leas()
        {
            fetch_effective_address();
            s.LowWord = ea.LowWord;
            int_state |= 32;
        }
        void leau()
        {
            fetch_effective_address();
            u.LowWord = ea.LowWord;
        }
        void pshs()
        {
            byte t;
            t = IMMBYTE();
            if ((t & 0x80) != 0)
            {
                PUSHWORD(pc);
                pendingCycles -= 2;
            }
            if ((t & 0x40) != 0)
            {
                PUSHWORD(u);
                pendingCycles -= 2;
            }
            if ((t & 0x20) != 0)
            {
                PUSHWORD(y);
                pendingCycles -= 2;
            }
            if ((t & 0x10) != 0)
            {
                PUSHWORD(x);
                pendingCycles -= 2;
            }
            if ((t & 0x08) != 0)
            {
                PUSHBYTE(dp.HighByte);
                pendingCycles -= 1;
            }
            if ((t & 0x04) != 0)
            {
                PUSHBYTE(d.LowByte);
                pendingCycles -= 1;
            }
            if ((t & 0x02) != 0)
            {
                PUSHBYTE(d.HighByte);
                pendingCycles -= 1;
            }
            if ((t & 0x01) != 0)
            {
                PUSHBYTE(cc);
                pendingCycles -= 1;
            }
        }
        void pshsw()
        {
            PUSHWORD(w);
        }
        void pshuw()
        {
            PSHUWORD(w);
        }
        void puls()
        {
            byte t;
            t = IMMBYTE();
            if ((t & 0x01) != 0)
            {
                cc = PULLBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x02) != 0)
            {
                d.HighByte = PULLBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x04) != 0)
            {
                d.LowByte = PULLBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x08) != 0)
            {
                dp.HighByte = PULLBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x10) != 0)
            {
                x.d = PULLWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x20) != 0)
            {
                y.d = PULLWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x40) != 0)
            {
                u.d = PULLWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x80) != 0)
            {
                pc.d = PULLWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x01) != 0)
            {
                CHECK_IRQ_LINES();
            }
        }
        void pulsw()
        {
            w.LowWord = PULLWORD();
        }
        void puluw()
        {
            w.LowWord = PULUWORD();
        }
        void pshu()
        {
            byte t;
            t = IMMBYTE();
            if ((t & 0x80) != 0)
            {
                PSHUWORD(pc);
                pendingCycles -= 2;
            }
            if ((t & 0x40) != 0)
            {
                PSHUWORD(s);
                pendingCycles -= 2;
            }
            if ((t & 0x20) != 0)
            {
                PSHUWORD(y);
                pendingCycles -= 2;
            }
            if ((t & 0x10) != 0)
            {
                PSHUWORD(x);
                pendingCycles -= 2;
            }
            if ((t & 0x08) != 0)
            {
                PSHUBYTE(dp.HighByte);
                pendingCycles -= 1;
            }
            if ((t & 0x04) != 0)
            {
                PSHUBYTE(d.LowByte);
                pendingCycles -= 1;
            }
            if ((t & 0x02) != 0)
            {
                PSHUBYTE(d.HighByte);
                pendingCycles -= 1;
            }
            if ((t & 0x01) != 0)
            {
                PSHUBYTE(cc);
                pendingCycles -= 1;
            }
        }

        void pulu()
        {
            byte t;
            t = IMMBYTE();
            if ((t & 0x01) != 0)
            {
                cc = PULUBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x02) != 0)
            {
                d.HighByte = PULUBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x04) != 0)
            {
                d.LowByte = PULUBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x08) != 0)
            {
                dp.HighByte = PULUBYTE();
                pendingCycles -= 1;
            }
            if ((t & 0x10) != 0)
            {
                x.d = PULUWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x20) != 0)
            {
                y.d = PULUWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x40) != 0)
            {
                s.d = PULUWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x80) != 0)
            {
                pc.d = PULUWORD();
                pendingCycles -= 2;
            }
            if ((t & 0x01) != 0)
            {
                CHECK_IRQ_LINES();
            }
        }
        void rts()
        {
            pc.d = PULLWORD();
        }
        void abx()
        {
            x.LowWord += d.LowByte;
        }
        void rti()
        {
            byte t;
            cc = PULLBYTE();
            t = (byte)(cc & CC_E);
            if (t != 0)
            {
                pendingCycles -= 9;
                d.HighByte = PULLBYTE();
                d.LowByte = PULLBYTE();
                if ((md & MD_EM) != 0)
                {
                    w.HighByte = PULLBYTE();
                    w.LowByte = PULLBYTE();
                    pendingCycles -= 2;
                }
                dp.HighByte = PULLBYTE();
                x.d = PULLWORD();
                y.d = PULLWORD();
                u.d = PULLWORD();
            }
            pc.d = PULLWORD();
            CHECK_IRQ_LINES();
        }
        void cwai()
        {
            byte t;
            t = IMMBYTE();
            cc &= t;
            cc |= CC_E;
            PUSHWORD(pc);
            PUSHWORD(u);
            PUSHWORD(y);
            PUSHWORD(x);
            PUSHBYTE(dp.HighByte);
            if ((md & MD_EM) != 0)
            {
                PUSHBYTE(w.HighByte);
                PUSHBYTE(w.LowByte);
            }
            PUSHBYTE(d.LowByte);
            PUSHBYTE(d.HighByte);
            PUSHBYTE(cc);
            int_state |= 8;
            CHECK_IRQ_LINES();
            if ((int_state & 8) != 0)
            {
                if (pendingCycles > 0)
                {
                    pendingCycles = 0;
                }
            }
        }
        void mul()
        {
            ushort t;
            t = (ushort)(d.HighByte * d.LowByte);
            CLR_ZC();
            SET_Z16(t);
            if ((t & 0x80) != 0)
            {
                SEC();
            }
            d.LowWord = t;
        }
        void swi()
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
            }
            PUSHBYTE(d.LowByte);
            PUSHBYTE(d.HighByte);
            PUSHBYTE(cc);
            cc |= (byte)(CC_IF | CC_II);
            pc.d = RM16(0xfffa);
        }
        int decodePB_tReg(byte n)
        {
            return (byte)(((n) >> 6) & 0x03);
        }
        int decodePB_src(byte n)
        {
            return (byte)(((n) >> 3) & 0x07);
        }
        int decodePB_dst(byte n)
        {
            return (byte)(((n) >> 0) & 0x07);
        }
        byte getregTable(int i)
        {
            byte b1 = 0;
            if (i == 0)
            {
                b1 = cc;
            }
            else if (i == 1)
            {
                b1 = d.HighByte;
            }
            else if (i == 2)
            {
                b1 = d.LowByte;
            }
            else if (i == 3)
            {
                b1 = dummy_byte;
            }
            return b1;
        }
        void setregTable(int i, byte b)
        {
            if (i == 0)
            {
                cc = b;
            }
            else if (i == 1)
            {
                d.HighByte = b;
            }
            else if (i == 2)
            {
                d.LowByte = b;
            }
            else if (i == 3)
            {
                dummy_byte = b;
            }
        }
        void band()
        {
            byte pb;
            ushort db;
            pb = IMMBYTE();
            db = DIRBYTE();
            if ((getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]) != 0 && (db & bitTable[decodePB_src(pb)]) != 0)
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void biand()
        {
            byte pb;
            ushort db;
            pb = IMMBYTE();
            db = DIRBYTE();
            if ((getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]) != 0 && ((~db) & bitTable[decodePB_src(pb)]) != 0)
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void bor()
        {
            byte pb;
            ushort db;
            pb = IMMBYTE();
            db = DIRBYTE();
            if ((getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]) != 0 || (db & bitTable[decodePB_src(pb)]) != 0)
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void bior()
        {
            byte pb;
            ushort db;
            pb = IMMBYTE();
            db = DIRBYTE();
            if ((getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]) != 0 || ((~db) & bitTable[decodePB_src(pb)]) != 0)
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void beor()
        {
            byte pb;
            ushort db;
            byte tReg, tMem;
            pb = IMMBYTE();
            db = DIRBYTE();
            tReg = (byte)(getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]);
            tMem = (byte)(db & bitTable[decodePB_src(pb)]);
            if ((tReg != 0 || tMem != 0) && !(tReg != 0 && tMem != 0))
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void bieor()
        {
            byte pb;
            ushort db;
            byte tReg, tMem;
            pb = IMMBYTE();
            db = DIRBYTE();
            tReg = (byte)(getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]);
            tMem = (byte)((~db) & bitTable[decodePB_src(pb)]);
            if ((tReg != 0 || tMem != 0) && !(tReg != 0 && tMem != 0))
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void ldbt()
        {
            byte pb;
            ushort db;
            pb = IMMBYTE();
            db = DIRBYTE();
            if ((db & bitTable[decodePB_src(pb)]) != 0)
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) | bitTable[decodePB_dst(pb)]));
            }
            else
            {
                setregTable(decodePB_tReg(pb), (byte)(getregTable(decodePB_tReg(pb)) & (~bitTable[decodePB_dst(pb)])));
            }
        }
        void stbt()
        {
            byte pb;
            ushort db;
            pb = IMMBYTE();
            db = DIRBYTE();
            if ((getregTable(decodePB_tReg(pb)) & bitTable[decodePB_dst(pb)]) != 0)
            {
                WM(ea.LowWord, (byte)(db | bitTable[decodePB_src(pb)]));
            }
            else
            {
                WM(ea.LowWord, (byte)(db & (~bitTable[decodePB_src(pb)])));
            }
        }
        void swi2()
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
            }
            PUSHBYTE(d.LowByte);
            PUSHBYTE(d.HighByte);
            PUSHBYTE(cc);
            pc.d = RM16(0xfff4);
        }
        void swi3()
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
            }
            PUSHBYTE(d.LowByte);
            PUSHBYTE(d.HighByte);
            PUSHBYTE(cc);
            pc.d = RM16(0xfff2);
        }
        void nega()
        {
            ushort r;
            r = (ushort)(-d.HighByte);
            CLR_NZVC();
            SET_FLAGS8(0, d.HighByte, r);
            d.HighByte = (byte)r;
        }
        void coma()
        {
            d.HighByte = (byte)(~d.HighByte);
            CLR_NZV();
            SET_NZ8(d.HighByte);
            SEC();
        }
        void lsra()
        {
            CLR_NZC();
            cc |= (byte)(d.HighByte & CC_C);
            d.HighByte >>= 1;
            SET_Z8(d.HighByte);
        }
        void rora()
        {
            byte r;
            r = (byte)((cc & CC_C) << 7);
            CLR_NZC();
            cc |= (byte)(d.HighByte & CC_C);
            r |= (byte)(d.HighByte >> 1);
            SET_NZ8(r);
            d.HighByte = r;
        }
        void asra()
        {
            CLR_NZC();
            cc |= (byte)(d.HighByte & CC_C);
            d.HighByte = (byte)((d.HighByte & 0x80) | (d.HighByte >> 1));
            SET_NZ8(d.HighByte);
        }
        void asla()
        {
            ushort r;
            r = (ushort)(d.HighByte << 1);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, d.HighByte, r);
            d.HighByte = (byte)r;
        }
        void rola()
        {
            ushort t, r;
            t = d.HighByte;
            r = (ushort)((cc & CC_C) | (t << 1));
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            d.HighByte = (byte)r;
        }
        void deca()
        {
            --d.HighByte;
            CLR_NZV();
            SET_FLAGS8D(d.HighByte);
        }
        void inca()
        {
            ++d.HighByte;
            CLR_NZV();
            SET_FLAGS8I(d.HighByte);
        }
        void tsta()
        {
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void clra()
        {
            d.HighByte = 0;
            CLR_NZVC();
            SEZ();
        }
        void negb()
        {
            ushort r;
            r = (ushort)(-d.LowByte);
            CLR_NZVC();
            SET_FLAGS8(0, d.LowByte, r);
            d.LowByte = (byte)r;
        }
        void negd()
        {
            uint r;
            r = (uint)(-d.LowWord);
            CLR_NZVC();
            SET_FLAGS16(0, d.LowWord, r);
            d.LowWord = (byte)r;
        }
        void comb()
        {
            d.LowByte = (byte)(~d.LowByte);
            CLR_NZV();
            SET_NZ8(d.LowByte);
            SEC();
        }
        void come()
        {
            w.HighByte = (byte)(~w.HighByte);
            CLR_NZV();
            SET_NZ8(w.HighByte);
            SEC();
        }
        void comf()
        {
            w.LowByte = (byte)(~w.LowByte);
            CLR_NZV();
            SET_NZ8(w.LowByte);
            SEC();
        }

        void comd()
        {
            d.LowWord = (ushort)(~d.LowWord);
            CLR_NZV();
            SET_NZ16(d.LowWord);
            SEC();
        }
        void comw()
        {
            w.LowWord = (ushort)(~w.LowWord);
            CLR_NZV();
            SET_NZ16(w.LowWord);
            SEC();
        }
        void lsrb()
        {
            CLR_NZC();
            cc |= (byte)(d.LowByte & CC_C);
            d.LowByte >>= 1;
            SET_Z8(d.LowByte);
        }
        void lsrd()
        {
            CLR_NZC();
            cc |= (byte)(d.LowByte & CC_C);
            d.LowWord >>= 1;
            SET_Z16(d.LowWord);
        }
        void lsrw()
        {
            CLR_NZC();
            cc |= (byte)(w.LowByte & CC_C);
            w.LowWord >>= 1;
            SET_Z16(w.LowWord);
        }
        void rorb()
        {
            byte r;
            r = (byte)((cc & CC_C) << 7);
            CLR_NZC();
            cc |= (byte)(d.LowByte & CC_C);
            r |= (byte)(d.LowByte >> 1);
            SET_NZ8(r);
            d.LowByte = r;
        }
        void rord()
        {
            ushort r;
            r = (ushort)((cc & CC_C) << 15);
            CLR_NZC();
            cc |= (byte)(d.LowWord & CC_C);
            r |= (ushort)(d.LowWord >> 1);
            SET_NZ16(r);
            d.LowWord = r;
        }
        void rorw()
        {
            ushort r;
            r = (ushort)((cc & CC_C) << 15);
            CLR_NZC();
            cc |= (byte)(w.LowWord & CC_C);
            r |= (ushort)(w.LowWord >> 1);
            SET_NZ16(r);
            w.LowWord = r;
        }
        void asrb()
        {
            CLR_NZC();
            cc |= (byte)(d.LowByte & CC_C);
            d.LowByte = (byte)((d.LowByte & 0x80) | (d.LowByte >> 1));
            SET_NZ8(d.LowByte);
        }
        void asrd()
        {
            CLR_NZC();
            cc |= (byte)(d.LowWord & CC_C);
            d.LowWord = (ushort)((d.LowWord & 0x8000) | (d.LowWord >> 1));
            SET_NZ16(d.LowWord);
        }
        void aslb()
        {
            ushort r;
            r = (ushort)(d.LowByte << 1);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, d.LowByte, r);
            d.LowByte = (byte)r;
        }
        void asld()
        {
            uint r;
            r = (uint)(d.LowWord << 1);
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, d.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void rolb()
        {
            ushort t, r;
            t = d.LowByte;
            r = (ushort)(cc & CC_C);
            r |= (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            d.LowByte = (byte)r;
        }
        void rold()
        {
            uint t, r;
            t = d.LowWord;
            r = (uint)(cc & CC_C);
            r |= t << 1;
            CLR_NZVC();
            SET_FLAGS16((ushort)t, (ushort)t, r);
            d.LowWord = (ushort)r;
        }
        void rolw()
        {
            uint t, r;
            t = w.LowWord;
            r = (uint)(cc & CC_C);
            r |= t << 1;
            CLR_NZVC();
            SET_FLAGS16((ushort)t, (ushort)t, r);
            w.LowWord = (ushort)r;
        }
        void decb()
        {
            --d.LowByte;
            CLR_NZV();
            SET_FLAGS8D(d.LowByte);
        }
        void dece()
        {
            --w.HighByte;
            CLR_NZV();
            SET_FLAGS8D(w.HighByte);
        }
        void decf()
        {
            --w.LowByte;
            CLR_NZV();
            SET_FLAGS8D(w.LowByte);
        }
        void decd()
        {
            uint r;
            r = (uint)(d.LowWord - 1);
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, d.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void decw()
        {
            uint r;
            r = (uint)(w.LowWord - 1);
            CLR_NZVC();
            SET_FLAGS16(w.LowWord, w.LowWord, r);
            w.LowWord = (ushort)r;
        }
        void incb()
        {
            ++d.LowByte;
            CLR_NZV();
            SET_FLAGS8I(d.LowByte);
        }
        void ince()
        {
            ++w.HighByte;
            CLR_NZV();
            SET_FLAGS8I(w.HighByte);
        }
        void incf()
        {
            ++w.LowByte;
            CLR_NZV();
            SET_FLAGS8I(w.LowByte);
        }
        void incd()
        {
            uint r;
            r = (uint)(d.LowWord + 1);
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, d.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void incw()
        {
            uint r;
            r = (uint)(w.LowWord + 1);
            CLR_NZVC();
            SET_FLAGS16(w.LowWord, w.LowWord, r);
            w.LowWord = (ushort)r;
        }
        void tstb()
        {
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void tstd()
        {
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void tstw()
        {
            CLR_NZV();
            SET_NZ16(w.LowWord);
        }
        void tste()
        {
            CLR_NZV();
            SET_NZ8(w.HighByte);
        }
        void tstf()
        {
            CLR_NZV();
            SET_NZ8(w.LowByte);
        }
        void clrb()
        {
            d.LowByte = 0;
            CLR_NZVC();
            SEZ();
        }
        void clrd()
        {
            d.LowWord = 0;
            CLR_NZVC();
            SEZ();
        }
        void clre()
        {
            w.HighByte = 0;
            CLR_NZVC();
            SEZ();
        }
        void clrf()
        {
            w.LowByte = 0;
            CLR_NZVC();
            SEZ();
        }
        void clrw()
        {
            w.LowWord = 0;
            CLR_NZVC();
            SEZ();
        }
        void neg_ix()
        {
            ushort r, t;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(-t);
            CLR_NZVC();
            SET_FLAGS8(0, t, r);
            WM((ushort)ea.d, (byte)r);
        }
        void oim_ix()
        {
            byte r, im;
            im = IMMBYTE();
            fetch_effective_address();
            r = (byte)(im | RM((ushort)ea.d));
            CLR_NZV();
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void aim_ix()
        {
            byte r, im;
            im = IMMBYTE();
            fetch_effective_address();
            r = (byte)(im & RM((ushort)ea.d));
            CLR_NZV();
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void com_ix()
        {
            byte t;
            fetch_effective_address();
            t = (byte)(~RM((ushort)ea.d));
            CLR_NZV();
            SET_NZ8(t);
            SEC();
            WM((ushort)ea.d, t);
        }
        void lsr_ix()
        {
            byte t;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            t >>= 1; SET_Z8(t);
            WM((ushort)ea.d, t);
        }
        void eim_ix()
        {
            byte r, im;
            im = IMMBYTE();
            fetch_effective_address();
            r = (byte)(im ^ RM((ushort)ea.d));
            CLR_NZV();
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void ror_ix()
        {
            byte t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (byte)((cc & CC_C) << 7);
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            r |= (byte)(t >> 1);
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void asr_ix()
        {
            byte t;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            t = (byte)((t & 0x80) | (t >> 1));
            SET_NZ8(t);
            WM((ushort)ea.d, t);
        }
        void asl_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            WM((ushort)ea.d, (byte)r);
        }
        void rol_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(cc & CC_C);
            r |= (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            WM((ushort)ea.d, (byte)r);
        }
        void dec_ix()
        {
            byte t;
            fetch_effective_address();
            t = (byte)(RM((ushort)ea.d) - 1);
            CLR_NZV();
            SET_FLAGS8D(t);
            WM((ushort)ea.d, t);
        }
        void tim_ix()
        {
            byte r, im, m;
            im = IMMBYTE();
            fetch_effective_address();
            m = RM((ushort)ea.d);
            r = (byte)(im & m);
            CLR_NZV();
            SET_NZ8(r);
        }
        void inc_ix()
        {
            byte t;
            fetch_effective_address();
            t = (byte)(RM((ushort)ea.d) + 1);
            CLR_NZV();
            SET_FLAGS8I(t);
            WM((ushort)ea.d, t);
        }
        void tst_ix()
        {
            byte t;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(t);
        }
        void jmp_ix()
        {
            fetch_effective_address();
            pc.d = (ushort)ea.d;
        }
        void clr_ix()
        {
            uint dummy;
            fetch_effective_address();
            dummy = RM((ushort)ea.d);
            WM((ushort)ea.d, 0);
            CLR_NZVC();
            SEZ();
        }
        void neg_ex()
        {
            ushort r, t;
            t = EXTBYTE();
            r = (ushort)(-t);
            CLR_NZVC();
            SET_FLAGS8(0, t, r);
            WM((ushort)ea.d, (byte)r);
        }
        void oim_ex()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = EXTBYTE();
            r = (byte)(im | t);
            CLR_NZV();
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void aim_ex()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = EXTBYTE();
            r = (byte)(im & t);
            CLR_NZV();
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void com_ex()
        {
            byte t;
            t = EXTBYTE();
            t = (byte)(~t);
            CLR_NZV();
            SET_NZ8(t);
            SEC();
            WM((ushort)ea.d, t);
        }
        void lsr_ex()
        {
            byte t;
            t = EXTBYTE();
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            t >>= 1;
            SET_Z8(t);
            WM((ushort)ea.d, t);
        }
        void eim_ex()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = EXTBYTE();
            r = (byte)(im ^ t);
            CLR_NZV();
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void ror_ex()
        {
            byte t, r;
            t = EXTBYTE();
            r = (byte)((cc & CC_C) << 7);
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            r |= (byte)(t >> 1);
            SET_NZ8(r);
            WM((ushort)ea.d, r);
        }
        void asr_ex()
        {
            byte t;
            t = EXTBYTE();
            CLR_NZC();
            cc |= (byte)(t & CC_C);
            t = (byte)((t & 0x80) | (t >> 1));
            SET_NZ8(t);
            WM((ushort)ea.d, t);
        }
        void asl_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(t << 1);
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            WM((ushort)ea.d, (byte)r);
        }
        void rol_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)((cc & CC_C) | (t << 1));
            CLR_NZVC();
            SET_FLAGS8((byte)t, t, r);
            WM((ushort)ea.d, (byte)r);
        }
        void dec_ex()
        {
            byte t;
            t = EXTBYTE();
            --t;
            CLR_NZV();
            SET_FLAGS8D(t);
            WM((ushort)ea.d, t);
        }
        void tim_ex()
        {
            byte r, t, im;
            im = IMMBYTE();
            t = EXTBYTE();
            r = (byte)(im & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void inc_ex()
        {
            byte t;
            t = EXTBYTE();
            ++t;
            CLR_NZV();
            SET_FLAGS8I(t);
            WM((ushort)ea.d, t);
        }
        void tst_ex()
        {
            byte t;
            t = EXTBYTE();
            CLR_NZV();
            SET_NZ8(t);
        }
        void jmp_ex()
        {
            EXTENDED();
            pc.d = ea.d;
        }
        void clr_ex()
        {
            uint dummy;
            EXTENDED();
            dummy = RM((ushort)ea.d);
            WM((ushort)ea.d, 0);
            CLR_NZVC();
            SEZ();
        }
        void suba_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void cmpa_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
        }
        void sbca_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.HighByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void subd_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void subw_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void cmpd_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpw_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpu_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = u.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void anda_im()
        {
            byte t;
            t = IMMBYTE();
            d.HighByte &= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void bita_im()
        {
            byte t, r;
            t = IMMBYTE();
            r = (byte)(d.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_im()
        {
            d.HighByte = IMMBYTE();
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void eora_im()
        {
            byte t;
            t = IMMBYTE();
            d.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adca_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.HighByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void ora_im()
        {
            byte t;
            t = IMMBYTE();
            d.HighByte |= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adda_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void cmpx_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = x.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpy_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = y.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmps_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = s.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void bsr()
        {
            byte t;
            t = IMMBYTE();
            PUSHWORD(pc);
            pc.LowWord += SIGNED(t);
        }
        void ldx_im()
        {
            x = IMMWORD();
            CLR_NZV();
            SET_NZ16(x.LowWord);
        }
        void ldq_im()
        {
            Register q = new Register();
            q = IMMLONG();
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void ldy_im()
        {
            y = IMMWORD();
            CLR_NZV();
            SET_NZ16(y.LowWord);
        }
        void muld_im()
        {
            Register t = new Register(), q = new Register();
            t = IMMWORD();
            q.d = (uint)((short)d.LowWord * (short)t.LowWord);
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZVC();
            SET_NZ16(d.LowWord);
        }
        void divd_im()
        {
            byte t;
            short v, oldD;
            t = IMMBYTE();
            if (t != 0)
            {
                oldD = (short)d.LowWord;
                v = (short)((short)d.LowWord / (sbyte)t);
                d.HighByte = (byte)((short)d.LowWord % (sbyte)t);
                d.LowByte = (byte)v;
                CLR_NZVC();
                SET_NZ8(d.LowByte);
                if ((d.LowByte & 0x01) != 0)
                {
                    SEC();
                }
                if ((short)d.LowWord < 0)
                {
                    SEN();
                }
                if ((v > 127) || (v < -128))
                {
                    SEV();
                    if ((v > 255) || (v < -256))
                    {
                        SET_NZ16((ushort)oldD);
                        d.LowWord = (ushort)Math.Abs(oldD);
                    }
                }
            }
            else
            {
                pendingCycles -= 8;
                DZError();
            }
        }
        void divq_im()
        {
            Register t = new Register(), q = new Register(), oldQ = new Register();
            int v;
            t = IMMWORD();
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            if (t.LowWord != 0)
            {
                oldQ = q;
                v = (int)q.d / (short)t.LowWord;
                d.LowWord = (ushort)((int)q.d % (short)t.LowWord);
                w.LowWord = (ushort)v;
                CLR_NZVC();
                SET_NZ16(w.LowWord);
                if ((w.LowWord & 0x0001) != 0)
                {
                    SEC();
                }
                if ((v > 32768) || (v < -35767))
                {
                    SEV();
                    if ((v > 65536) || (v < -65535))
                    {
                        if ((int)oldQ.d < 0)
                        {
                            SEN();
                        }
                        else if (oldQ.d == 0)
                        {
                            SEZ();
                        }
                        t.LowWord = (ushort)Math.Abs(t.LowWord);
                        d.LowWord = oldQ.HighWord;
                        w.LowWord = oldQ.LowWord;
                    }
                }
            }
            else
            {
                DZError();
            }
        }
        void suba_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void cmpa_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
        }

        void sbca_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.HighByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }

        void subd_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }

        void subw_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }

        void cmpd_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }

        void cmpw_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }

        void cmpu_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = u.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16(u.LowWord, (ushort)b.d, r);
        }
        void anda_di()
        {
            byte t;
            t = DIRBYTE();
            d.HighByte &= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void bita_di()
        {
            byte t, r;
            t = DIRBYTE();
            r = (byte)(d.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_di()
        {
            d.HighByte = DIRBYTE();
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void sta_di()
        {
            CLR_NZV();
            SET_NZ8(d.HighByte);
            DIRECT();
            WM((ushort)ea.d, d.HighByte);
        }
        void eora_di()
        {
            byte t;
            t = DIRBYTE();
            d.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adca_di()
        {
            ushort t, r;
            t = DIRBYTE(); ;
            r = (ushort)(d.HighByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void ora_di()
        {
            byte t;
            t = DIRBYTE();
            d.HighByte |= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adda_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void cmpx_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = x.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpy_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = y.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmps_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = s.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void jsr_di()
        {
            DIRECT();
            PUSHWORD(pc);
            pc.d = ea.d;
        }
        void ldx_di()
        {
            x = DIRWORD();
            CLR_NZV();
            SET_NZ16(x.LowWord);
        }
        void muld_di()
        {
            Register t = new Register(), q = new Register();
            t = DIRWORD();
            q.d = (uint)((short)d.LowWord * (short)t.LowWord);
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZVC();
            SET_NZ16(d.LowWord);
        }
        void divd_di()
        {
            byte t;
            short v, oldD;
            t = DIRBYTE();
            if (t != 0)
            {
                oldD = (short)d.LowWord;
                v = (short)((short)d.LowWord / (sbyte)t);
                d.HighByte = (byte)((short)d.LowWord % (sbyte)t);
                d.LowByte = (byte)v;
                CLR_NZVC();
                SET_NZ8(d.LowByte);
                if ((d.LowByte & 0x01) != 0)
                {
                    SEC();
                }
                if ((short)d.LowWord < 0)
                {
                    SEN();
                }
                if ((v > 127) || (v < -128))
                {
                    SEV();
                    if ((v > 255) || (v < -256))
                    {
                        SET_NZ16((ushort)oldD);
                        d.LowWord = (ushort)Math.Abs(oldD);
                    }
                }
            }
            else
            {
                pendingCycles -= 8;
                DZError();
            }
        }
        void divq_di()
        {
            Register t = new Register(), q = new Register(), oldQ = new Register();
            int v;
            t = DIRWORD();
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            if (t.LowWord != 0)
            {
                oldQ = q;
                v = (int)q.d / (short)t.LowWord;
                d.LowWord = (ushort)((int)q.d % (short)t.LowWord);
                w.LowWord = (ushort)v;
                CLR_NZVC();
                SET_NZ16(w.LowWord);
                if ((w.LowWord & 0x0001) != 0)
                {
                    SEC();
                }
                if ((v > 32767) || (v < -32768))
                {
                    SEV();
                    if ((v > 65535) || (v < -65536))
                    {
                        if ((int)oldQ.d < 0)
                        {
                            SEN();
                        }
                        else if (oldQ.d == 0)
                        {
                            SEZ();
                        }
                        t.LowWord = (ushort)Math.Abs(t.LowWord);
                        d.LowWord = oldQ.HighWord;
                        w.LowWord = oldQ.LowWord;
                    }
                }
            }
            else
            {
                DZError();
            }
        }
        void ldq_di()
        {
            Register q = new Register();
            q = DIRLONG();
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void ldy_di()
        {
            y = DIRWORD();
            CLR_NZV();
            SET_NZ16(y.LowWord);
        }
        void stx_di()
        {
            CLR_NZV();
            SET_NZ16(x.LowWord);
            DIRECT();
            WM16((ushort)ea.d, x);
        }
        void stq_di()
        {
            Register q = new Register();
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            DIRECT();
            WM32((ushort)ea.d, q);
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void sty_di()
        {
            CLR_NZV();
            SET_NZ16(y.LowWord);
            DIRECT();
            WM16((ushort)ea.d, y);
        }
        void suba_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void cmpa_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
        }
        void sbca_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.HighByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void subd_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void subw_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void cmpd_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpw_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpu_ix()
        {
            uint r;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            r = u.LowWord - b.d;
            CLR_NZVC();
            SET_FLAGS16(u.LowWord, (ushort)b.d, r);
        }
        void anda_ix()
        {
            fetch_effective_address();
            d.HighByte &= RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void bita_ix()
        {
            byte r;
            fetch_effective_address();
            r = (byte)(d.HighByte & RM((ushort)ea.d));
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_ix()
        {
            fetch_effective_address();
            d.HighByte = RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void sta_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ8(d.HighByte);
            WM((ushort)ea.d, d.HighByte);
        }
        void eora_ix()
        {
            fetch_effective_address();
            d.HighByte ^= RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adca_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.HighByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void ora_ix()
        {
            fetch_effective_address();
            d.HighByte |= RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adda_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void cmpx_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = x.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpy_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = y.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmps_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = s.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void jsr_ix()
        {
            fetch_effective_address();
            PUSHWORD(pc);
            pc.d = ea.d;
        }
        void ldx_ix()
        {
            fetch_effective_address();
            x.LowWord = RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(x.LowWord);
        }
        void muld_ix()
        {
            Register q = new Register();
            ushort t;
            fetch_effective_address();
            t = RM16((ushort)ea.d);
            q.d = (uint)((short)d.LowWord * (short)t);
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZVC();
            SET_NZ16(d.LowWord);
        }
        void divd_ix()
        {
            byte t;
            short v, oldD;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            if (t != 0)
            {
                oldD = (short)d.LowWord;
                v = (short)((short)d.LowWord / (sbyte)t);
                d.HighByte = (byte)((short)d.LowWord % (sbyte)t);
                d.LowByte = (byte)v;
                CLR_NZVC();
                SET_NZ8(d.LowByte);
                if ((d.LowByte & 0x01) != 0)
                {
                    SEC();
                }
                if ((short)d.LowWord < 0)
                {
                    SEN();
                }
                if ((v > 127) || (v < -128))
                {
                    SEV();

                    if ((v > 255) || (v < -256))
                    {
                        SET_NZ16((ushort)oldD);
                        d.LowWord = (ushort)Math.Abs(oldD);
                    }
                }
            }
            else
            {
                pendingCycles -= 8;
                DZError();
            }
        }
        void divq_ix()
        {
            Register t = new Register(), q = new Register(), oldQ = new Register();
            int v;
            fetch_effective_address();
            t.LowWord = RM16((ushort)ea.d);
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            if (t.LowWord != 0)
            {
                oldQ = q;
                v = (int)q.d / (short)t.LowWord;
                d.LowWord = (ushort)((int)q.d % (short)t.LowWord);
                w.LowWord = (ushort)v;
                CLR_NZVC();
                SET_NZ16(w.LowWord);
                if ((w.LowWord & 0x0001) != 0)
                {
                    SEC();
                }
                if ((v > 32767) || (v < -32768))
                {
                    SEV();
                    if ((v > 65535) || (v < -65536))
                    {
                        if ((int)oldQ.d < 0)
                        {
                            SEN();
                        }
                        else if (oldQ.d == 0)
                        {
                            SEZ();
                        }
                        t.LowWord = (ushort)Math.Abs(t.LowWord);
                        d.LowWord = oldQ.HighWord;
                        w.LowWord = oldQ.LowWord;
                    }
                }
            }
            else
            {
                DZError();
            }
        }
        void ldq_ix()
        {
            Register q = new Register();
            fetch_effective_address();
            q.d = RM32((ushort)ea.d);
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void ldy_ix()
        {
            fetch_effective_address();
            y.LowWord = RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(y.LowWord);
        }
        void stx_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ16(x.LowWord);
            WM16((ushort)ea.d, x);
        }
        void stq_ix()
        {
            Register q = new Register();
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            fetch_effective_address();
            WM32((ushort)ea.d, q);
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void sty_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ16(y.LowWord);
            WM16((ushort)ea.d, y);
        }
        void suba_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void cmpa_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
        }
        void sbca_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.HighByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.HighByte, t, r);
            d.HighByte = (byte)r;
        }
        void subd_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void subw_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void cmpd_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = d.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpw_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = w.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpu_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = u.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void anda_ex()
        {
            byte t;
            t = EXTBYTE();
            d.HighByte &= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void bita_ex()
        {
            byte t, r;
            t = EXTBYTE();
            r = (byte)(d.HighByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void lda_ex()
        {
            d.HighByte = EXTBYTE();
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void sta_ex()
        {
            CLR_NZV();
            SET_NZ8(d.HighByte);
            EXTENDED();
            WM((ushort)ea.d, d.HighByte);
        }
        void eora_ex()
        {
            byte t;
            t = EXTBYTE();
            d.HighByte ^= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adca_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.HighByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void ora_ex()
        {
            byte t;
            t = EXTBYTE();
            d.HighByte |= t;
            CLR_NZV();
            SET_NZ8(d.HighByte);
        }
        void adda_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.HighByte, t, r);
            SET_H(d.HighByte, (byte)t, (byte)r);
            d.HighByte = (byte)r;
        }
        void cmpx_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = x.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmpy_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = y.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void cmps_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = s.LowWord;
            r = d1 - b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
        }
        void jsr_ex()
        {
            EXTENDED();
            PUSHWORD(pc);
            pc.d = ea.d;
        }
        void ldx_ex()
        {
            x = EXTWORD();
            CLR_NZV();
            SET_NZ16(x.LowWord);
        }
        void muld_ex()
        {
            Register t = new Register(), q = new Register();
            t = EXTWORD();
            q.d = (uint)((short)d.LowWord * (short)t.LowWord);
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZVC();
            SET_NZ16(d.LowWord);
        }
        void divd_ex()
        {
            byte t;
            short v, oldD;
            t = EXTBYTE();
            if (t != 0)
            {
                oldD = (short)d.LowWord;
                v = (short)((short)d.LowWord / (sbyte)t);
                d.HighByte = (byte)((short)d.LowWord % (sbyte)t);
                d.LowByte = (byte)v;
                CLR_NZVC();
                SET_NZ8(d.LowByte);
                if ((d.LowByte & 0x01) != 0)
                {
                    SEC();
                }
                if ((short)d.LowWord < 0)
                {
                    SEN();
                }
                if ((v > 127) || (v < -128))
                {
                    SEV();
                    if ((v > 255) || (v < -256))
                    {
                        SET_NZ16((ushort)oldD);
                        d.LowWord = (ushort)Math.Abs(oldD);
                    }
                }
            }
            else
            {
                pendingCycles -= 8;
                DZError();
            }
        }
        void divq_ex()
        {
            Register t = new Register(), q = new Register(), oldQ = new Register();
            int v;
            t = EXTWORD();
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            if (t.LowWord != 0)
            {
                oldQ = q;
                v = (int)q.d / (short)t.LowWord;
                d.LowWord = (ushort)((int)q.d % (short)t.LowWord);
                w.LowWord = (ushort)v;
                CLR_NZVC();
                SET_NZ16(w.LowWord);
                if ((w.LowWord & 0x0001) != 0)
                {
                    SEC();
                }
                if ((v > 32767) || (v < -32768))
                {
                    SEV();
                    if ((v > 65535) || (v < -65536))
                    {
                        if ((int)oldQ.d < 0)
                        {
                            SEN();
                        }
                        else if (oldQ.d == 0)
                        {
                            SEZ();
                        }
                        t.LowWord = (ushort)Math.Abs(t.LowWord);
                        d.LowWord = oldQ.HighWord;
                        w.LowWord = oldQ.LowWord;
                    }
                }
            }
            else
            {
                DZError();
            }
        }
        void ldq_ex()
        {
            Register q = new Register();
            q = EXTLONG();
            d.LowWord = q.HighWord;
            w.LowWord = q.LowWord;
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void ldy_ex()
        {
            y = EXTWORD();
            CLR_NZV();
            SET_NZ16(y.LowWord);
        }
        void stx_ex()
        {
            CLR_NZV();
            SET_NZ16(x.LowWord);
            EXTENDED();
            WM16((ushort)ea.d, x);
        }
        void stq_ex()
        {
            Register q = new Register();
            q.HighWord = d.LowWord;
            q.LowWord = w.LowWord;
            EXTENDED();
            WM32((ushort)ea.d, q);
            CLR_NZV();
            SET_N8(d.HighByte);
            SET_Z(q.d);
        }
        void sty_ex()
        {
            CLR_NZV();
            SET_NZ16(y.LowWord);
            EXTENDED();
            WM16((ushort)ea.d, y);
        }
        void subb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sube_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
            w.HighByte = (byte)r;
        }
        void subf_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
            w.LowByte = (byte)r;
        }
        void cmpb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
        }
        void cmpe_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
        }
        void cmpf_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
        }
        void sbcb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.LowByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sbcd_im()
        {
            Register t = new Register();
            uint r;
            t = IMMWORD();
            r = (uint)(d.LowWord - t.LowWord - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, t.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void addd_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = d.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void addw_im()
        {
            uint r, d1;
            Register b = new Register();
            b = IMMWORD();
            d1 = w.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void adde_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(w.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.HighByte, t, r);
            SET_H(w.HighByte, (byte)t, (byte)r);
            w.HighByte = (byte)r;
        }
        void addf_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(w.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.LowByte, t, r);
            SET_H(w.LowByte, (byte)t, (byte)r);
            w.LowByte = (byte)r;
        }
        void andb_im()
        {
            byte t;
            t = IMMBYTE();
            d.LowByte &= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void andd_im()
        {
            Register t = new Register();
            t = IMMWORD();
            d.LowWord &= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void bitb_im()
        {
            byte t, r;
            t = IMMBYTE();
            r = (byte)(d.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void bitd_im()
        {
            Register t = new Register();
            ushort r;
            t = IMMWORD();
            r = (ushort)(d.LowByte & t.LowWord);
            CLR_NZV();
            SET_NZ16(r);
        }
        void bitmd_im()
        {
            byte t, r;
            t = IMMBYTE();
            r = (byte)(md & t);
            CLR_Z();
            SET_Z8(r);
            md &= (byte)(~(r & 0xc0));
        }
        void ldb_im()
        {
            d.LowByte = IMMBYTE();
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void ldmd_im()
        {
            md = IMMBYTE();
            UpdateState();
        }
        void lde_im()
        {
            w.LowByte = IMMBYTE();
            CLR_NZV();
            SET_NZ8(w.HighByte);
        }
        void ldf_im()
        {
            w.LowByte = IMMBYTE();
            CLR_NZV();
            SET_NZ8(w.LowByte);
        }
        void eorb_im()
        {
            byte t;
            t = IMMBYTE();
            d.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void eord_im()
        {
            Register t = new Register();
            t = IMMWORD();
            d.LowWord ^= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void adcb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.LowByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void adcd_im()
        {
            Register t = new Register();
            uint r;
            t = IMMWORD();
            r = (uint)(d.LowWord + t.LowWord + (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, t.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void orb_im()
        {
            byte t;
            t = IMMBYTE();
            d.LowByte |= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void ord_im()
        {
            Register t = new Register();
            t = IMMWORD();
            d.LowWord |= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void addb_im()
        {
            ushort t, r;
            t = IMMBYTE();
            r = (ushort)(d.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void ldd_im()
        {
            Register t = new Register();
            t = IMMWORD();
            d.LowWord = t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void ldw_im()
        {
            Register t = new Register();
            t = IMMWORD();
            w.LowWord = t.LowWord;
            CLR_NZV();
            SET_NZ16(w.LowWord);
        }
        void ldu_im()
        {
            u = IMMWORD();
            CLR_NZV();
            SET_NZ16(u.LowWord);
        }
        void lds_im()
        {
            s = IMMWORD();
            CLR_NZV();
            SET_NZ16(s.LowWord);
            int_state |= 32;
        }
        void subb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sube_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
            w.HighByte = (byte)r;
        }
        void subf_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
            w.LowByte = (byte)r;
        }
        void cmpb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
        }
        void cmpe_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
        }
        void cmpf_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
        }
        void sbcb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.LowByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sbcd_di()
        {
            Register t = new Register();
            uint r;
            t = DIRWORD();
            r = (uint)(d.LowWord - t.LowWord - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, t.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void addd_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = d.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void addw_di()
        {
            uint r, d1;
            Register b = new Register();
            b = DIRWORD();
            d1 = w.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void adde_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(w.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.HighByte, t, r);
            SET_H(w.HighByte, (byte)t, (byte)r);
            w.HighByte = (byte)r;
        }
        void addf_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(w.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.LowByte, t, r);
            SET_H(w.LowByte, (byte)t, (byte)r);
            w.LowByte = (byte)r;
        }
        void andb_di()
        {
            byte t;
            t = DIRBYTE();
            d.LowByte &= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void andd_di()
        {
            Register t = new Register();
            t = DIRWORD();
            d.LowWord &= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void bitb_di()
        {
            byte t, r;
            t = DIRBYTE();
            r = (byte)(d.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void bitd_di()
        {
            Register t = new Register();
            ushort r;
            t = DIRWORD();
            r = (ushort)(d.LowByte & t.LowWord);
            CLR_NZV();
            SET_NZ16(r);
        }
        void ldb_di()
        {
            d.LowByte = DIRBYTE();
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void lde_di()
        {
            w.HighByte = DIRBYTE();
            CLR_NZV();
            SET_NZ8(w.HighByte);
        }
        void ldf_di()
        {
            w.LowByte = DIRBYTE();
            CLR_NZV();
            SET_NZ8(w.LowByte);
        }
        void stb_di()
        {
            CLR_NZV();
            SET_NZ8(d.LowByte);
            DIRECT();
            WM((ushort)ea.d, d.LowByte);
        }
        void ste_di()
        {
            CLR_NZV();
            SET_NZ8(w.HighByte);
            DIRECT();
            WM((ushort)ea.d, w.HighByte);
        }
        void stf_di()
        {
            CLR_NZV();
            SET_NZ8(w.LowByte);
            DIRECT();
            WM((ushort)ea.d, w.LowByte);
        }
        void eorb_di()
        {
            byte t;
            t = DIRBYTE();
            d.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void eord_di()
        {
            Register t = new Register();
            t = DIRWORD();
            d.LowWord ^= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void adcb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.LowByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void adcd_di()
        {
            uint r;
            Register t = new Register();
            t = DIRWORD();
            r = (uint)(d.LowWord + t.LowWord + (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, t.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void orb_di()
        {
            byte t;
            t = DIRBYTE();
            d.LowByte |= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void ord_di()
        {
            Register t = new Register();
            t = DIRWORD();
            d.LowWord |= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void addb_di()
        {
            ushort t, r;
            t = DIRBYTE();
            r = (ushort)(d.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void ldd_di()
        {
            Register t = new Register();
            t = DIRWORD();
            d.LowWord = t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void ldw_di()
        {
            Register t = new Register();
            t = DIRWORD();
            w.LowWord = t.LowWord;
            CLR_NZV();
            SET_NZ16(w.LowWord);
        }
        void std_di()
        {
            CLR_NZV();
            SET_NZ16(d.LowWord);
            DIRECT();
            WM16((ushort)ea.d, d);
        }
        void stw_di()
        {
            CLR_NZV();
            SET_NZ16(w.LowWord);
            DIRECT();
            WM16((ushort)ea.d, w);
        }
        void ldu_di()
        {
            u = DIRWORD();
            CLR_NZV();
            SET_NZ16(u.LowWord);
        }
        void lds_di()
        {
            s = DIRWORD();
            CLR_NZV();
            SET_NZ16(s.LowWord);
            int_state |= 32;
        }
        void stu_di()
        {
            CLR_NZV();
            SET_NZ16(u.LowWord);
            DIRECT();
            WM16((ushort)ea.d, u);
        }
        void sts_di()
        {
            CLR_NZV();
            SET_NZ16(s.LowWord);
            DIRECT();
            WM16((ushort)ea.d, s);
        }
        void subb_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sube_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
            w.HighByte = (byte)r;
        }
        void subf_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
            w.LowByte = (byte)r;
        }
        void cmpb_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
        }
        void cmpe_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
        }
        void cmpf_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
        }
        void sbcb_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.LowByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sbcd_ix()
        {
            uint t, r;
            fetch_effective_address();
            t = RM16((ushort)ea.d);
            r = (uint)(d.LowWord - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, (ushort)t, r);
            d.LowWord = (ushort)r;
        }
        void addd_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = d.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void addw_ix()
        {
            uint r, d1;
            Register b = new Register();
            fetch_effective_address();
            b.d = RM16((ushort)ea.d);
            d1 = w.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void adde_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(w.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.HighByte, t, r);
            SET_H(w.HighByte, (byte)t, (byte)r);
            w.HighByte = (byte)r;
        }
        void addf_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(w.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.LowByte, t, r);
            SET_H(w.LowByte, (byte)t, (byte)r);
            w.LowByte = (byte)r;
        }
        void andb_ix()
        {
            fetch_effective_address();
            d.LowByte &= RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void andd_ix()
        {
            fetch_effective_address();
            d.LowWord &= RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void bitb_ix()
        {
            byte r;
            fetch_effective_address();
            r = (byte)(d.LowByte & RM((ushort)ea.d));
            CLR_NZV();
            SET_NZ8(r);
        }
        void bitd_ix()
        {
            ushort r;
            fetch_effective_address();
            r = (ushort)(d.LowWord & RM16((ushort)ea.d));
            CLR_NZV();
            SET_NZ16(r);
        }
        void ldb_ix()
        {
            fetch_effective_address();
            d.LowByte = RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void lde_ix()
        {
            fetch_effective_address();
            w.HighByte = RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(w.HighByte);
        }
        void ldf_ix()
        {
            fetch_effective_address();
            w.LowByte = RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(w.LowByte);
        }
        void stb_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ8(d.LowByte);
            WM((ushort)ea.d, d.LowByte);
        }
        void ste_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ8(w.HighByte);
            WM((ushort)ea.d, w.HighByte);
        }
        void stf_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ8(w.LowByte);
            WM((ushort)ea.d, w.LowByte);
        }
        void eorb_ix()
        {
            fetch_effective_address();
            d.LowByte ^= RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void eord_ix()
        {
            fetch_effective_address();
            d.LowWord ^= RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void adcb_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.LowByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void adcd_ix()
        {
            uint r;
            Register t = new Register();
            fetch_effective_address();
            t.d = RM16((ushort)ea.d);
            r = (uint)(d.LowWord + t.d + (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, (ushort)t.d, r);
            d.LowWord = (ushort)r;
        }
        void orb_ix()
        {
            fetch_effective_address();
            d.LowByte |= RM((ushort)ea.d);
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void ord_ix()
        {
            fetch_effective_address();
            d.LowWord |= RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void addb_ix()
        {
            ushort t, r;
            fetch_effective_address();
            t = RM((ushort)ea.d);
            r = (ushort)(d.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void ldd_ix()
        {
            fetch_effective_address();
            d.LowWord = RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void ldw_ix()
        {
            fetch_effective_address();
            w.LowWord = RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(w.LowWord);
        }
        void std_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ16(d.LowWord);
            WM16((ushort)ea.d, d);
        }
        void stw_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ16(w.LowWord);
            WM16((ushort)ea.d, w);
        }
        void ldu_ix()
        {
            fetch_effective_address();
            u.LowWord = RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(u.LowWord);
        }
        void lds_ix()
        {
            fetch_effective_address();
            s.LowWord = RM16((ushort)ea.d);
            CLR_NZV();
            SET_NZ16(s.LowWord);
            int_state |= 32;
        }
        void stu_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ16(u.LowWord);
            WM16((ushort)ea.d, u);
        }
        void sts_ix()
        {
            fetch_effective_address();
            CLR_NZV();
            SET_NZ16(s.LowWord);
            WM16((ushort)ea.d, s);
        }
        void subb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sube_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
            w.HighByte = (byte)r;
        }
        void subf_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
            w.LowByte = (byte)r;
        }
        void cmpb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
        }
        void cmpe_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(w.HighByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.HighByte, t, r);
        }
        void cmpf_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(w.LowByte - t);
            CLR_NZVC();
            SET_FLAGS8(w.LowByte, t, r);
        }
        void sbcb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.LowByte - t - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS8(d.LowByte, t, r);
            d.LowByte = (byte)r;
        }
        void sbcd_ex()
        {
            Register t = new Register();
            uint r;
            t = EXTWORD();
            r = (uint)(d.LowWord - t.LowWord - (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, t.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void addd_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = d.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            d.LowWord = (ushort)r;
        }
        void addw_ex()
        {
            uint r, d1;
            Register b = new Register();
            b = EXTWORD();
            d1 = w.LowWord;
            r = d1 + b.d;
            CLR_NZVC();
            SET_FLAGS16((ushort)d1, (ushort)b.d, r);
            w.LowWord = (ushort)r;
        }
        void adde_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(w.HighByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.HighByte, t, r);
            SET_H(w.HighByte, (byte)t, (byte)r);
            w.HighByte = (byte)r;
        }
        void addf_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(w.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(w.LowByte, t, r);
            SET_H(w.LowByte, (byte)t, (byte)r);
            w.LowByte = (byte)r;
        }
        void andb_ex()
        {
            byte t;
            t = EXTBYTE();
            d.LowByte &= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void andd_ex()
        {
            Register t = new Register();
            t = EXTWORD();
            d.LowWord &= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void bitb_ex()
        {
            byte t, r;
            t = EXTBYTE();
            r = (byte)(d.LowByte & t);
            CLR_NZV();
            SET_NZ8(r);
        }
        void bitd_ex()
        {
            Register t = new Register();
            byte r;
            t = EXTWORD();
            r = (byte)(d.LowByte & t.LowWord);
            CLR_NZV();
            SET_NZ16(r);
        }
        void ldb_ex()
        {
            d.LowByte = EXTBYTE();
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void lde_ex()
        {
            w.HighByte = EXTBYTE();
            CLR_NZV();
            SET_NZ8(w.HighByte);
        }
        void ldf_ex()
        {
            w.LowByte = EXTBYTE();
            CLR_NZV();
            SET_NZ8(w.LowByte);
        }
        void stb_ex()
        {
            CLR_NZV();
            SET_NZ8(d.LowByte);
            EXTENDED();
            WM((ushort)ea.d, d.LowByte);
        }
        void ste_ex()
        {
            CLR_NZV();
            SET_NZ8(w.HighByte);
            EXTENDED();
            WM((ushort)ea.d, w.HighByte);
        }
        void stf_ex()
        {
            CLR_NZV();
            SET_NZ8(w.LowByte);
            EXTENDED();
            WM((ushort)ea.d, w.LowByte);
        }
        void eorb_ex()
        {
            byte t;
            t = EXTBYTE();
            d.LowByte ^= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void eord_ex()
        {
            Register t = new Register();
            t = EXTWORD();
            d.LowWord ^= t.LowWord;
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void adcb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.LowByte + t + (cc & CC_C));
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void adcd_ex()
        {
            uint r;
            Register t = new Register();
            t = EXTWORD();
            r = (uint)(d.LowWord + t.LowWord + (cc & CC_C));
            CLR_NZVC();
            SET_FLAGS16(d.LowWord, t.LowWord, r);
            d.LowWord = (ushort)r;
        }
        void orb_ex()
        {
            byte t;
            t = EXTBYTE();
            d.LowByte |= t;
            CLR_NZV();
            SET_NZ8(d.LowByte);
        }
        void ord_ex()
        {
            Register t = new Register();
            t = EXTWORD();
            d.LowWord |= t.LowWord;
            CLR_NZV();
            SET_NZ8((byte)d.LowWord);
        }
        void addb_ex()
        {
            ushort t, r;
            t = EXTBYTE();
            r = (ushort)(d.LowByte + t);
            CLR_HNZVC();
            SET_FLAGS8(d.LowByte, t, r);
            SET_H(d.LowByte, (byte)t, (byte)r);
            d.LowByte = (byte)r;
        }
        void ldd_ex()
        {
            d = EXTWORD();
            CLR_NZV();
            SET_NZ16(d.LowWord);
        }
        void ldw_ex()
        {
            w = EXTWORD();
            CLR_NZV();
            SET_NZ16(w.LowWord);
        }
        void std_ex()
        {
            CLR_NZV();
            SET_NZ16(d.LowWord);
            EXTENDED();
            WM16((ushort)ea.d, d);
        }
        void stw_ex()
        {
            CLR_NZV();
            SET_NZ16(w.LowWord);
            EXTENDED();
            WM16((ushort)ea.d, w);
        }
        void ldu_ex()
        {
            u = EXTWORD();
            CLR_NZV();
            SET_NZ16(u.LowWord);
        }
        void lds_ex()
        {
            s = EXTWORD();
            CLR_NZV();
            SET_NZ16(s.LowWord);
            int_state |= 32;
        }
        void stu_ex()
        {
            CLR_NZV();
            SET_NZ16(u.LowWord);
            EXTENDED();
            WM16((ushort)ea.d, u);
        }
        void sts_ex()
        {
            CLR_NZV();
            SET_NZ16(s.LowWord);
            EXTENDED();
            WM16((ushort)ea.d, s);
        }
        void pref10()
        {
            byte ireg2 = ReadOp(pc.LowWord);
            pc.LowWord++;
            hd6309_page01[ireg2]();
            pendingCycles -= cycle_counts_page01[ireg2];
        }
        void pref11()
        {
            byte ireg2 = ReadOp(pc.LowWord);
            pc.LowWord++;
            hd6309_page11[ireg2]();
            pendingCycles -= cycle_counts_page11[ireg2];
        }

    }
}
