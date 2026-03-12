using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public class Hd63484
    {
        public static int fifo_counter;
        public static ushort[] fifo;
        public static ushort readfifo;
        public static ushort[] HD63484_ram;
        public static ushort[] HD63484_reg;
        public static ushort[] pattern;
        public static int org, org_dpd, rwp;
        public static ushort cl0, cl1, ccmp, edg, mask, ppy, pzcy, ppx, pzcx, psy, psx, pey, pzy, pex, pzx, xmin, ymin, xmax, ymax, rwp_dn;
        public static short cpx, cpy;
        public static int regno;
        public static int[] instruction_length = new int[64]
        {
	         0, 3, 2, 1,	/* 0x */
	         0, 0,-1, 2,	/* 1x */
	         0, 3, 3, 3,	/* 2x */
	         0, 0, 0, 0,	/* 3x */
	         0, 1, 2, 2,	/* 4x */
	         0, 0, 4, 4,	/* 5x */
	         5, 5, 5, 5,	/* 6x */
	         5, 5, 5, 5,	/* 7x */
	         3, 3, 3, 3, 	/* 8x */
	         3, 3,-2,-2,	/* 9x */
	        -2,-2, 2, 4,	/* Ax */
	         5, 5, 7, 7,	/* Bx */
	         3, 3, 1, 1,	/* Cx */
	         2, 2, 2, 2,	/* Dx */
	         5, 5, 5, 5,	/* Ex */
	         5, 5, 5, 5 	/* Fx */
        };
        public static void HD63484_start()
        {
            int i;
            fifo_counter = 0;
            fifo = new ushort[0x100];
            HD63484_ram = new ushort[0x100000];
            HD63484_reg = new ushort[0x80];
            pattern = new ushort[0x10];
            for (i = 0; i < 0x100000; i++)
            {
                HD63484_ram[i] = 0;
            }
        }
        public static void doclr16(int opcode, ushort fill, ref int dst, short _ax, short _ay)
        {
            short ax, ay;
            ax = _ax;
            ay = _ay;
            for (; ; )
            {
                for (; ; )
                {
                    switch (opcode & 0x0003)
                    {
                        case 0:
                            HD63484_ram[dst] = fill;
                            break;
                        case 1:
                            HD63484_ram[dst] |= fill;
                            break;
                        case 2:
                            HD63484_ram[dst] &= fill;
                            break;
                        case 3:
                            HD63484_ram[dst] ^= fill;
                            break;
                    }
                    if (ax == 0)
                    {
                        break;
                    }
                    else if (ax > 0)
                    {
                        dst = (dst + 1) & (0x100000 - 1);
                        ax--;
                    }
                    else
                    {
                        dst = (dst - 1) & (0x100000 - 1);
                        ax++;
                    }
                }
                ax = _ax;
                if (_ay < 0)
                {
                    dst = (dst + (HD63484_reg[0xca / 2] & 0x0fff) - ax) & (0x100000 - 1);
                    if (ay == 0)
                    {
                        break;
                    }
                    ay++;
                }
                else
                {
                    dst = (dst - (HD63484_reg[0xca / 2] & 0x0fff) - ax) & (0x100000 - 1);
                    if (ay == 0)
                    {
                        break;
                    }
                    ay--;
                }
            }
        }
        public static void docpy16(int opcode, int src, ref int dst, short _ax, short _ay)
        {
            int dstep1, dstep2;
            int ax = _ax;
            int ay = _ay;
            switch (opcode & 0x0700)
            {
                default:
                case 0x0000: dstep1 = 1; dstep2 = -1 * (HD63484_reg[0xca / 2] & 0x0fff) - ax * dstep1; break;
                case 0x0100: dstep1 = 1; dstep2 = (HD63484_reg[0xca / 2] & 0x0fff) - ax * dstep1; break;
                case 0x0200: dstep1 = -1; dstep2 = -1 * (HD63484_reg[0xca / 2] & 0x0fff) + ax * dstep1; break;
                case 0x0300: dstep1 = -1; dstep2 = (HD63484_reg[0xca / 2] & 0x0fff) + ax * dstep1; break;
                case 0x0400: dstep1 = -1 * (HD63484_reg[0xca / 2] & 0x0fff); dstep2 = 1 - ay * dstep1; break;
                case 0x0500: dstep1 = (HD63484_reg[0xca / 2] & 0x0fff); dstep2 = 1 - ay * dstep1; break;
                case 0x0600: dstep1 = -1 * (HD63484_reg[0xca / 2] & 0x0fff); dstep2 = -1 + ay * dstep1; break;
                case 0x0700: dstep1 = (HD63484_reg[0xca / 2] & 0x0fff); dstep2 = -1 + ay * dstep1; break;
            }
            for (; ; )
            {
                for (; ; )
                {
                    switch (opcode & 0x0007)
                    {
                        case 0:
                            HD63484_ram[dst] = HD63484_ram[src];
                            break;
                        case 1:
                            HD63484_ram[dst] |= HD63484_ram[src];
                            break;
                        case 2:
                            HD63484_ram[dst] &= HD63484_ram[src];
                            break;
                        case 3:
                            HD63484_ram[dst] ^= HD63484_ram[src];
                            break;
                        case 4:
                            if (HD63484_ram[dst] == (ccmp & 0xff))
                                HD63484_ram[dst] = HD63484_ram[src];
                            break;
                        case 5:
                            if (HD63484_ram[dst] != (ccmp & 0xff))
                                HD63484_ram[dst] = HD63484_ram[src];
                            break;
                        case 6:
                            if (HD63484_ram[dst] < HD63484_ram[src])
                                HD63484_ram[dst] = HD63484_ram[src];
                            break;
                        case 7:
                            if (HD63484_ram[dst] > HD63484_ram[src])
                                HD63484_ram[dst] = HD63484_ram[src];
                            break;
                    }
                    if ((opcode & 0x0800) != 0)
                    {
                        if (ay == 0)
                        {
                            break;
                        }
                        if (_ay > 0)
                        {
                            src = (src - (HD63484_reg[0xca / 2] & 0x0fff)) & (0x100000 - 1);
                            dst = (dst + dstep1) & (0x100000 - 1);
                            ay--;
                        }
                        else
                        {
                            src = (src + (HD63484_reg[0xca / 2] & 0x0fff)) & (0x100000 - 1);
                            dst = (dst + dstep1) & (0x100000 - 1);
                            ay++;
                        }
                    }
                    else
                    {
                        if (ax == 0)
                        {
                            break;
                        }
                        else if (ax > 0)
                        {
                            src = (src + 1) & (0x100000 - 1);
                            dst = (dst + dstep1) & (0x100000 - 1);
                            ax--;
                        }
                        else
                        {
                            src = (src - 1) & (0x100000 - 1);
                            dst = (dst + dstep1) & (0x100000 - 1);
                            ax++;
                        }
                    }
                }
                if ((opcode & 0x0800) != 0)
                {
                    ay = _ay;
                    if (_ax < 0)
                    {
                        src = (src - 1 + ay * (HD63484_reg[0xca / 2] & 0x0fff)) & (0x100000 - 1);
                        dst = (dst + dstep2) & (0x100000 - 1);
                        if (ax == 0)
                        {
                            break;
                        }
                        ax++;
                    }
                    else
                    {
                        src = (src + 1 - ay * (HD63484_reg[0xca / 2] & 0x0fff)) & (0x100000 - 1);
                        dst = (dst + dstep2) & (0x100000 - 1);
                        if (ax == 0)
                        {
                            break;
                        }
                        ax--;
                    }
                }
                else
                {
                    ax = _ax;
                    if (_ay < 0)
                    {
                        src = (src + (HD63484_reg[0xca / 2] & 0x0fff) - ax) & (0x100000 - 1);
                        dst = (dst + dstep2) & (0x100000 - 1);
                        if (ay == 0)
                        {
                            break;
                        }
                        ay++;
                    }
                    else
                    {
                        src = (src - (HD63484_reg[0xca / 2] & 0x0fff) - ax) & (0x100000 - 1);
                        dst = (dst + dstep2) & (0x100000 - 1);
                        if (ay == 0)
                        {
                            break;
                        }
                        ay--;
                    }
                }
            }
        }
        public static int org_first_pixel(int _org_dpd)
        {
            int gbm = (HD63484_reg[0x02 / 2] & 0x700) >> 8;
            switch (gbm)
            {
                case 0:
                    return (_org_dpd & 0x0f);
                case 1:
                    return (_org_dpd & 0x0e) >> 1;
                case 2:
                    return (_org_dpd & 0x0c) >> 2;
                case 3:
                    return (_org_dpd & 0x08) >> 3;
                case 4:
                    return 0;
                default:
                    return 0;
            }
        }
        public static void dot(int x, int y, int opm, ushort color)
        {
            int dst, x_int, x_mod, bpp;
            ushort color_shifted, bitmask, bitmask_shifted;
            x += org_first_pixel(org_dpd);
            switch ((HD63484_reg[0x02 / 2] & 0x700) >> 8)
            {
                case 0:
                    bpp = 1;
                    bitmask = 0x0001;
                    break;
                case 1:
                    bpp = 2;
                    bitmask = 0x0003;
                    break;
                case 2:
                    bpp = 4;
                    bitmask = 0x000f;
                    break;
                case 3:
                    bpp = 8;
                    bitmask = 0x00ff;
                    break;
                case 4:
                    bpp = 16;
                    bitmask = 0xffff;
                    break;
                default:
                    bpp = 0;
                    bitmask = 0x0000;
                    break;
            }
            if (x >= 0)
            {
                x_int = x / (16 / bpp);
                x_mod = x % (16 / bpp);
            }
            else
            {
                x_int = x / (16 / bpp);
                x_mod = -1 * (x % (16 / bpp));
                if (x_mod != 0)
                {
                    x_int--;
                    x_mod = (16 / bpp) - x_mod;
                }
            }
            color &= bitmask;
            bitmask_shifted = (ushort)(bitmask << (x_mod * bpp));
            color_shifted = (ushort)(color << (x_mod * bpp));
            dst = (org + x_int - y * (HD63484_reg[0xca / 2] & 0x0fff)) & (0x100000 - 1);
            switch (opm)
            {
                case 0:
                    HD63484_ram[dst] = (ushort)((HD63484_ram[dst] & ~bitmask_shifted) | color_shifted);
                    break;
                case 1:
                    HD63484_ram[dst] = (ushort)(HD63484_ram[dst] | color_shifted);
                    break;
                case 2:
                    HD63484_ram[dst] = (ushort)(HD63484_ram[dst] & ((HD63484_ram[dst] & ~bitmask_shifted) | color_shifted));
                    break;
                case 3:
                    HD63484_ram[dst] = (ushort)(HD63484_ram[dst] ^ color_shifted);
                    break;
                case 4:
                    if (get_pixel(x, y) == (ccmp & bitmask))
                        HD63484_ram[dst] = (ushort)((HD63484_ram[dst] & ~bitmask_shifted) | color_shifted);
                    break;
                case 5:
                    if (get_pixel(x, y) != (ccmp & bitmask))
                        HD63484_ram[dst] = (ushort)((HD63484_ram[dst] & ~bitmask_shifted) | color_shifted);
                    break;
                case 6:
                    if (get_pixel(x, y) < (cl0 & bitmask))
                        HD63484_ram[dst] = (ushort)((HD63484_ram[dst] & ~bitmask_shifted) | color_shifted);
                    break;
                case 7:
                    if (get_pixel(x, y) > (cl0 & bitmask))
                        HD63484_ram[dst] = (ushort)((HD63484_ram[dst] & ~bitmask_shifted) | color_shifted);
                    break;
            }
        }
        public static int get_pixel(int x, int y)
        {
            int dst, x_int, x_mod, bpp;
            ushort bitmask, bitmask_shifted;
            switch ((HD63484_reg[0x02 / 2] & 0x700) >> 8)
            {
                case 0:
                    bpp = 1;
                    bitmask = 0x0001;
                    break;
                case 1:
                    bpp = 2;
                    bitmask = 0x0003;
                    break;
                case 2:
                    bpp = 4;
                    bitmask = 0x000f;
                    break;
                case 3:
                    bpp = 8;
                    bitmask = 0x00ff;
                    break;
                case 4:
                    bpp = 16;
                    bitmask = 0xffff;
                    break;
                default:
                    bpp = 0;
                    bitmask = 0x0000;
                    break;
            }
            if (x >= 0)
            {
                x_int = x / (16 / bpp);
                x_mod = x % (16 / bpp);
            }
            else
            {
                x_int = x / (16 / bpp);
                x_mod = -1 * (x % (16 / bpp));
                if (x_mod != 0)
                {
                    x_int--;
                    x_mod = (16 / bpp) - x_mod;
                }
            }
            bitmask_shifted = (ushort)(bitmask << (x_mod * bpp));
            dst = (org + x_int - y * (HD63484_reg[0xca / 2] & 0x0fff)) & (0x100000 - 1);
            return ((HD63484_ram[dst] & bitmask_shifted) >> (x_mod * bpp));
        }
        public static int get_pixel_ptn(int x, int y)
        {
            int dst, x_int, x_mod, bpp;
            ushort bitmask, bitmask_shifted;
            bpp = 1;
            bitmask = 0x0001;
            if (x >= 0)
            {
                x_int = x / (16 / bpp);
                x_mod = x % (16 / bpp);
            }
            else
            {
                x_int = x / (16 / bpp);
                x_mod = -1 * (x % (16 / bpp));
                if (x_mod != 0)
                {
                    x_int--;
                    x_mod = (16 / bpp) - x_mod;
                }
            }
            bitmask_shifted = (ushort)(bitmask << (x_mod * bpp));
            dst = (x_int + y * 1);
            if (((pattern[dst] & bitmask_shifted) >> (x_mod * bpp)) != 0)
                return 1; //cl1
            else
                return 0; //cl0
        }
        public static void agcpy(int opcode, int src_x, int src_y, int dst_x, int dst_y, short _ax, short _ay)
        {
            int dst_step1_x, dst_step1_y, dst_step2_x, dst_step2_y;
            int src_step1_x, src_step1_y, src_step2_x, src_step2_y;
            int ax_neg, ay_neg;
            int ax = _ax;
            int ay = _ay;
            int xxs = src_x;
            int yys = src_y;
            int xxd = dst_x;
            int yyd = dst_y;
            if (ax < 0)
            {
                ax_neg = -1;
            }
            else
            {
                ax_neg = 1;
            }
            if (ay < 0)
            {
                ay_neg = -1;
            }
            else
            {
                ay_neg = 1;
            }
            if ((opcode & 0x0800) != 0)
                switch (opcode & 0x0700)
                {
                    default:
                    case 0x0000: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ay_neg * ay; dst_step2_y = 1; break;
                    case 0x0100: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ay_neg * ay; dst_step2_y = -1; break;
                    case 0x0200: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ay_neg * ay; dst_step2_y = 1; break;
                    case 0x0300: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ay_neg * ay; dst_step2_y = -1; break;
                    case 0x0400: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = 1; dst_step2_y = -ay_neg * ay; break;
                    case 0x0500: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = 1; dst_step2_y = ay_neg * ay; break;
                    case 0x0600: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = -1; dst_step2_y = -ay_neg * ay; break;
                    case 0x0700: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = -1; dst_step2_y = ay_neg * ay; break;
                }
            else
                switch (opcode & 0x0700)
                {
                    default:
                    case 0x0000: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ax_neg * ax; dst_step2_y = 1; break;
                    case 0x0100: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ax_neg * ax; dst_step2_y = -1; break;
                    case 0x0200: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ax_neg * ax; dst_step2_y = 1; break;
                    case 0x0300: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ax_neg * ax; dst_step2_y = -1; break;
                    case 0x0400: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = 1; dst_step2_y = ax_neg * ax; break;
                    case 0x0500: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = 1; dst_step2_y = -ax_neg * ax; break;
                    case 0x0600: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = -1; dst_step2_y = ax_neg * ax; break;
                    case 0x0700: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = -1; dst_step2_y = -ax_neg * ax; break;
                }

            if ((_ax >= 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = 1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = 1; }
            else if ((_ax >= 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = 1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = -1; }
            else if ((_ax < 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = -1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = 1; }
            else if ((_ax < 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = -1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = -1; }
            else if ((_ax >= 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = 1; src_step2_x = 1; src_step2_y = -ay; }
            else if ((_ax >= 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = -1; src_step2_x = 1; src_step2_y = -ay; }
            else if ((_ax < 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = 1; src_step2_x = -1; src_step2_y = -ay; }
            else // ((_ax < 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = -1; src_step2_x = -1; src_step2_y = -ay; }
            for (; ; )
            {
                for (; ; )
                {
                    dot(xxd, yyd, opcode & 0x0007, (ushort)get_pixel(xxs, yys));
                    if ((opcode & 0x0800) != 0)
                    {
                        if (ay == 0) break;
                        if (_ay > 0)
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ay--;
                        }
                        else
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ay++;
                        }
                    }
                    else
                    {
                        if (ax == 0) break;
                        else if (ax > 0)
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ax--;
                        }
                        else
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ax++;
                        }
                    }
                }
                if ((opcode & 0x0800) != 0)
                {
                    ay = _ay;
                    if (_ax < 0)
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ax == 0) break;
                        ax++;
                    }
                    else
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ax == 0) break;
                        ax--;
                    }
                }
                else
                {
                    ax = _ax;
                    if (_ay < 0)
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ay == 0) break;
                        ay++;
                    }
                    else
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ay == 0) break;
                        ay--;
                    }
                }
            }
        }
        public static void ptn(int opcode, int src_x, int src_y, short _ax, short _ay)
        {
            int dst_step1_x, dst_step1_y, dst_step2_x, dst_step2_y;
            int src_step1_x, src_step1_y, src_step2_x, src_step2_y;
            int ax = _ax;
            int ay = _ay;
            int ax_neg, ay_neg;
            int xxs = src_x;
            int yys = src_y;
            int xxd = cpx;
            int yyd = cpy;
            if (ax < 0)
            {
                ax_neg = -1;
            }
            else
            {
                ax_neg = 1;
            }
            if (ay < 0)
            {
                ay_neg = -1;
            }
            else
            {
                ay_neg = 1;
            }
            if ((opcode & 0x0800) != 0)
                switch (opcode & 0x0700)
                {
                    default:
                    case 0x0000: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ay_neg * ay; dst_step2_y = 1; break;
                    case 0x0100: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ay_neg * ay; dst_step2_y = -1; break;
                    case 0x0200: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ay_neg * ay; dst_step2_y = 1; break;
                    case 0x0300: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ay_neg * ay; dst_step2_y = -1; break;
                    case 0x0400: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = 1; dst_step2_y = -ay_neg * ay; break;
                    case 0x0500: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = 1; dst_step2_y = ay_neg * ay; break;
                    case 0x0600: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = -1; dst_step2_y = -ay_neg * ay; break;
                    case 0x0700: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = -1; dst_step2_y = ay_neg * ay; break;
                }
            else
                switch (opcode & 0x0700)
                {
                    default:
                    case 0x0000: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ax_neg * ax; dst_step2_y = 1; break;
                    case 0x0100: dst_step1_x = 1; dst_step1_y = 0; dst_step2_x = -ax_neg * ax; dst_step2_y = -1; break;
                    case 0x0200: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ax_neg * ax; dst_step2_y = 1; break;
                    case 0x0300: dst_step1_x = -1; dst_step1_y = 0; dst_step2_x = ax_neg * ax; dst_step2_y = -1; break;
                    case 0x0400: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = 1; dst_step2_y = ax_neg * ax; break;
                    case 0x0500: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = 1; dst_step2_y = -ax_neg * ax; break;
                    case 0x0600: dst_step1_x = 0; dst_step1_y = 1; dst_step2_x = -1; dst_step2_y = ax_neg * ax; break;
                    case 0x0700: dst_step1_x = 0; dst_step1_y = -1; dst_step2_x = -1; dst_step2_y = -ax_neg * ax; break;
                }

            if ((_ax >= 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = 1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = 1; }
            else if ((_ax >= 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = 1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = -1; }
            else if ((_ax < 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = -1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = 1; }
            else if ((_ax < 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0000))
            { src_step1_x = -1; src_step1_y = 0; src_step2_x = -ax; src_step2_y = -1; }
            else if ((_ax >= 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = 1; src_step2_x = 1; src_step2_y = -ay; }
            else if ((_ax >= 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = -1; src_step2_x = 1; src_step2_y = -ay; }
            else if ((_ax < 0) && (_ay >= 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = 1; src_step2_x = -1; src_step2_y = -ay; }
            else // ((_ax < 0) && (_ay < 0) && ((opcode & 0x0800) == 0x0800))
            { src_step1_x = 0; src_step1_y = -1; src_step2_x = -1; src_step2_y = -ay; }

            for (; ; )
            {
                for (; ; )
                {
                    dot(xxd, yyd, opcode & 0x0007, (ushort)get_pixel_ptn(xxs, yys));
                    if ((opcode & 0x0800) != 0)
                    {
                        if (ay == 0) break;
                        if (_ay > 0)
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ay--;
                        }
                        else
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ay++;
                        }
                    }
                    else
                    {
                        if (ax == 0) break;
                        else if (ax > 0)
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ax--;
                        }
                        else
                        {
                            xxs += src_step1_x;
                            yys += src_step1_y;
                            xxd += dst_step1_x;
                            yyd += dst_step1_y;
                            ax++;
                        }
                    }
                }
                if ((opcode & 0x0800) != 0)
                {
                    ay = _ay;
                    if (_ax < 0)
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ax == 0) break;
                        ax++;
                    }
                    else
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ax == 0) break;
                        ax--;
                    }
                }
                else
                {
                    ax = _ax;
                    if (_ay < 0)
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ay == 0) break;
                        ay++;
                    }
                    else
                    {
                        xxs += src_step2_x;
                        yys += src_step2_y;
                        xxd += dst_step2_x;
                        yyd += dst_step2_y;
                        if (ay == 0) break;
                        ay--;
                    }
                }
            }
        }
        public static void line(short sx, short sy, short ex, short ey, short col)
        {
            short ax, ay;
            int cpx_t = sx;
            int cpy_t = sy;
            ax = (short)(ex - sx);
            ay = (short)(ey - sy);
            if (Math.Abs(ax) >= Math.Abs(ay))
            {
                while (ax != 0)
                {
                    dot(cpx_t, cpy_t, col, cl0);
                    if (ax > 0)
                    {
                        cpx_t++;
                        ax--;
                    }
                    else
                    {
                        cpx_t--;
                        ax++;
                    }
                    cpy_t = sy + ay * (cpx_t - sx) / (ex - sx);
                }
            }
            else
            {
                while (ay != 0)
                {
                    dot(cpx_t, cpy_t, col, cl0);
                    if (ay > 0)
                    {
                        cpy_t++;
                        ay--;
                    }
                    else
                    {
                        cpy_t--;
                        ay++;
                    }
                    cpx_t = sx + ax * (cpy_t - sy) / (ey - sy);
                }
            }
        }
        public static void HD63484_command_w(ushort cmd)
        {
            int len;
            fifo[fifo_counter++] = cmd;
            len = instruction_length[fifo[0] >> 10];
            if (len == -1)
            {
                if (fifo_counter < 2) return;
                else len = fifo[1] + 2;
            }
            else if (len == -2)
            {
                if (fifo_counter < 2) return;
                else len = 2 * fifo[1] + 2;
            }
            if (fifo_counter >= len)
            {
                if (fifo[0] == 0x0400)
                { /* ORG */
                    org = ((fifo[1] & 0x00ff) << 12) | ((fifo[2] & 0xfff0) >> 4);
                    org_dpd = fifo[2] & 0x000f;
                }
                else if ((fifo[0] & 0xffe0) == 0x0800)	/* WPR */
                {
                    if (fifo[0] == 0x0800)
                        cl0 = fifo[1];
                    else if (fifo[0] == 0x0801)
                        cl1 = fifo[1];
                    else if (fifo[0] == 0x0802)
                        ccmp = fifo[1];
                    else if (fifo[0] == 0x0803)
                        edg = fifo[1];
                    else if (fifo[0] == 0x0804)
                        mask = fifo[1];
                    else if (fifo[0] == 0x0805)
                    {
                        ppy = (ushort)((fifo[1] & 0xf000) >> 12);
                        pzcy = (ushort)((fifo[1] & 0x0f00) >> 8);
                        ppx = (ushort)((fifo[1] & 0x00f0) >> 4);
                        pzcx = (ushort)((fifo[1] & 0x000f) >> 0);
                    }
                    else if (fifo[0] == 0x0806)
                    {
                        psy = (ushort)((fifo[1] & 0xf000) >> 12);
                        psx = (ushort)((fifo[1] & 0x00f0) >> 4);
                    }
                    else if (fifo[0] == 0x0807)
                    {
                        pey = (ushort)((fifo[1] & 0xf000) >> 12);
                        pzy = (ushort)((fifo[1] & 0x0f00) >> 8);
                        pex = (ushort)((fifo[1] & 0x00f0) >> 4);
                        pzx = (ushort)((fifo[1] & 0x000f) >> 0);
                    }
                    else if (fifo[0] == 0x0808)
                        xmin = fifo[1];
                    else if (fifo[0] == 0x0809)
                        ymin = fifo[1];
                    else if (fifo[0] == 0x080a)
                        xmax = fifo[1];
                    else if (fifo[0] == 0x080b)
                        ymax = fifo[1];
                    else if (fifo[0] == 0x080c)
                    {
                        rwp = (rwp & 0x00fff) | ((fifo[1] & 0x00ff) << 12);
                        rwp_dn = (ushort)((fifo[1] & 0xc000) >> 14);
                    }
                    else if (fifo[0] == 0x080d)
                        rwp = (rwp & 0xff000) | ((fifo[1] & 0xfff0) >> 4);
                }
                else if ((fifo[0] & 0xfff0) == 0x1800)	/* WPTN */
                {
                    int i;
                    int start = fifo[0] & 0x000f;
                    int n = fifo[1];
                    for (i = 0; i < n; i++)
                        pattern[start + i] = fifo[2 + i];
                }
                else if (fifo[0] == 0x4400)	/* RD */
                {
                    readfifo = HD63484_ram[rwp];
                    rwp = (rwp + 1) & (0x100000 - 1);
                }
                else if (fifo[0] == 0x4800)	/* WT */
                {
                    //if (!input_code_pressed(KEYCODE_9))
                    HD63484_ram[rwp] = fifo[1];
                    rwp = (rwp + 1) & (0x100000 - 1);
                }
                else if (fifo[0] == 0x5800)	/* CLR */
                {
                    doclr16(fifo[0], fifo[1], ref rwp, (short)fifo[2], (short)fifo[3]);
                }
                else if ((fifo[0] & 0xfffc) == 0x5c00)	/* SCLR */
                {
                    doclr16(fifo[0], fifo[1], ref rwp, (short)fifo[2], (short)fifo[3]);
                }
                else if ((fifo[0] & 0xf0ff) == 0x6000)	/* CPY */
                {
                    docpy16(fifo[0], ((fifo[1] & 0x00ff) << 12) | ((fifo[2] & 0xfff0) >> 4), ref rwp, (short)fifo[3], (short)fifo[4]);
                }
                else if ((fifo[0] & 0xf0fc) == 0x7000)	/* SCPY */
                {
                    docpy16(fifo[0], ((fifo[1] & 0x00ff) << 12) | ((fifo[2] & 0xfff0) >> 4), ref rwp, (short)fifo[3], (short)fifo[4]);
                }
                else if (fifo[0] == 0x8000)	/* AMOVE */
                {
                    cpx = (short)fifo[1];
                    cpy = (short)fifo[2];
                }
                else if (fifo[0] == 0x8400)	/* RMOVE */
                {
                    cpx += (short)fifo[1];
                    cpy += (short)fifo[2];
                }
                else if ((fifo[0] & 0xff00) == 0x8800)	/* ALINE */
                {
                    line(cpx, cpy, (short)fifo[1], (short)fifo[2], (short)(fifo[0] & 7));
                    cpx = (short)fifo[1];
                    cpy = (short)fifo[2];
                }
                else if ((fifo[0] & 0xff00) == 0x8c00)	/* RLINE */
                {
                    line(cpx, cpy, (short)(cpx + (short)fifo[1]), (short)(cpy + (short)fifo[2]), (short)(fifo[0] & 7));
                    cpx += (short)fifo[1];
                    cpy += (short)fifo[2];
                }
                else if ((fifo[0] & 0xfff8) == 0x9000)	/* ARCT */
                {
                    line(cpx, cpy, (short)fifo[1], cpy, (short)(fifo[0] & 7));
                    line((short)fifo[1], cpy, (short)fifo[1], (short)fifo[2], (short)(fifo[0] & 7));
                    line((short)fifo[1], (short)fifo[2], cpx, (short)fifo[2], (short)(fifo[0] & 7));
                    line(cpx, (short)fifo[2], cpx, cpy, (short)(fifo[0] & 7));
                    cpx = (short)fifo[1];
                    cpy = (short)fifo[2];
                }
                else if ((fifo[0] & 0xfff8) == 0x9400)	/* RRCT  added*/
                {
                    line(cpx, cpy, (short)(cpx + (short)fifo[1]), cpy, (short)(fifo[0] & 7));
                    line((short)(cpx + (short)fifo[1]), cpy, (short)(cpx + (short)fifo[1]), (short)(cpy + (short)fifo[2]), (short)(fifo[0] & 7));
                    line((short)(cpx + (short)fifo[1]), (short)(cpy + (short)fifo[2]), cpx, (short)(cpy + (short)fifo[2]), (short)(fifo[0] & 7));
                    line(cpx, (short)(cpy + (short)fifo[2]), cpx, cpy, (short)(fifo[0] & 7));
                    cpx += (short)fifo[1];
                    cpy += (short)fifo[2];
                }
                else if ((fifo[0] & 0xfff8) == 0xa400)	/* RPLG  added*/
                {
                    int nseg, sx, sy, ex, ey;
                    sx = cpx;
                    sy = cpy;
                    for (nseg = 0; nseg < fifo[1]; nseg++)
                    {
                        ex = sx + (short)fifo[2 + nseg * 2];
                        ey = sy + (short)fifo[2 + nseg * 2 + 1];
                        line((short)sx, (short)sy, (short)ex, (short)ey, (short)(fifo[0] & 7));
                        sx = ex;
                        sy = ey;
                    }
                    line((short)sx, (short)sy, cpx, cpy, (short)(fifo[0] & 7));
                }
                else if ((fifo[0] & 0xfff8) == 0xc000)	/* AFRCT */
                {
                    short pcx, pcy;
                    short ax, ay, xx, yy;
                    pcx = (short)fifo[1];
                    pcy = (short)fifo[2];
                    ax = (short)(pcx - cpx);
                    ay = (short)(pcy - cpy);
                    xx = cpx;
                    yy = cpy;
                    for (; ; )
                    {
                        for (; ; )
                        {
                            dot(xx, yy, fifo[0] & 0x0007, cl0);
                            if (ax == 0) break;
                            else if (ax > 0)
                            {
                                xx++;
                                ax--;
                            }
                            else
                            {
                                xx--;
                                ax++;
                            }
                        }
                        ax = (short)(pcx - cpx);
                        if (pcy < cpy)
                        {
                            yy--;
                            xx -= ax;
                            if (ay == 0) break;
                            ay++;
                        }
                        else
                        {
                            yy++;
                            xx -= ax;
                            if (ay == 0) break;
                            ay--;
                        }
                    }
                }
                else if ((fifo[0] & 0xfff8) == 0xc400)	/* RFRCT  added TODO*/
                {
                    line(cpx, cpy, (short)(cpx + (short)fifo[1]), cpy, (short)(fifo[0] & 7));
                    line((short)(cpx + fifo[1]), cpy, (short)(cpx + fifo[1]), (short)(cpy + fifo[2]), (short)(fifo[0] & 7));
                    line((short)(cpx + fifo[1]), (short)(cpy + fifo[2]), cpx, (short)(cpy + fifo[2]), (short)(fifo[0] & 7));
                    line(cpx, (short)(cpy + fifo[2]), cpx, cpy, (short)(fifo[0] & 7));
                    cpx = (short)(cpx + (short)fifo[1]);
                    cpy = (short)(cpy + (short)fifo[2]);
                }
                else if ((fifo[0] & 0xfff8) == 0xcc00)	/* DOT */
                {
                    dot(cpx, cpy, fifo[0] & 0x0007, cl0);
                }
                else if ((fifo[0] & 0xf000) == 0xd000)	/* PTN (to do) */
                {
                    ptn(fifo[0] & 0x0007, psx, psy, (short)(pex - psx), (short)(pey - psy));
                    cpx += (short)(pex - psx);
                    cpy += (short)(pey - psy);
                }
                else if ((fifo[0] & 0xf0f8) == 0xe000)	/* AGCPY */
                {
                    agcpy(fifo[0], fifo[1], fifo[2], cpx, cpy, (short)fifo[3], (short)fifo[4]);
                    cpx += (short)fifo[4];
                    cpy += (short)fifo[3];
                }
                else
                {

                }
                fifo_counter = 0;
            }
        }
        public static ushort HD63484_status_r()
        {
            return 0xff22;	/* write FIFO ready + command end    +  (read FIFO ready or read FIFO not ready) */
        }
        public static void HD63484_address_w2(byte data)
        {
            regno = data;
        }
        public static void HD63484_address_w(ushort data)
        {
            //if (ACCESSING_BITS_0_7)
            regno = data;
        }
        public static void HD63484_data_w1(byte data)
        {
            HD63484_reg[regno / 2] = (ushort)((data << 8) | (HD63484_reg[regno / 2] & 0xff));
            if ((regno & 0x80) != 0)
            {
                regno += 2;	/* autoincrement */
            }
            if (regno == 0)	/* FIFO */
            {
                HD63484_command_w(HD63484_reg[0]);
            }
        }
        public static void HD63484_data_w2(byte data)
        {
            HD63484_reg[regno / 2] = (ushort)((HD63484_reg[regno / 2] & 0xff00) | data);
            if ((regno & 0x80) != 0)
            {
                regno += 2;	/* autoincrement */
            }
            if (regno == 0)	/* FIFO */
            {
                HD63484_command_w(HD63484_reg[0]);
            }
        }
        public static void HD63484_data_w(ushort data)
        {
            HD63484_reg[regno / 2] = data;
            if ((regno & 0x80) != 0)
            {
                regno += 2;	/* autoincrement */
            }
            if (regno == 0)	/* FIFO */
            {
                HD63484_command_w(HD63484_reg[0]);
            }
        }
        public static ushort HD63484_data_r()
        {
            int res;
            if (regno == 0x80)
            {
                res = Video.video_screen_get_vpos();
            }
            else if (regno == 0)
            {
                res = readfifo;
            }
            else
            {
                res = 0;
            }
            return (ushort)res;
        }
    }
}
