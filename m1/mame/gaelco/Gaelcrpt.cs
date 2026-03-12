using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Gaelco
    {
        public static int lastpc, lastoffset, lastencword, lastdecword;
        public static int decrypt(int param1, int param2, int enc_prev_word, int dec_prev_word, int enc_word)
        {
            int swap = (BIT(dec_prev_word, 8) << 1) | BIT(dec_prev_word, 7);
            int type = (BIT(dec_prev_word, 12) << 1) | BIT(dec_prev_word, 2);
            int res = 0;
            int k = 0;
            switch (swap)
            {
                case 0: res = BITSWAP16(enc_word, 1, 2, 0, 14, 12, 15, 4, 8, 13, 7, 3, 6, 11, 5, 10, 9); break;
                case 1: res = BITSWAP16(enc_word, 14, 10, 4, 15, 1, 6, 12, 11, 8, 0, 9, 13, 7, 3, 5, 2); break;
                case 2: res = BITSWAP16(enc_word, 2, 13, 15, 1, 12, 8, 14, 4, 6, 0, 9, 5, 10, 7, 3, 11); break;
                case 3: res = BITSWAP16(enc_word, 3, 8, 1, 13, 14, 4, 15, 0, 10, 2, 7, 12, 6, 11, 9, 5); break;
            }
            res ^= param2;
            switch (type)
            {
                case 0:
                    k = (0 << 0) |
                        (1 << 1) |
                        (0 << 2) |
                        (1 << 3) |
                        (1 << 4) |
                        (1 << 5);
                    break;
                case 1:
                    k = (BIT(dec_prev_word, 0) << 0) |
                        (BIT(dec_prev_word, 1) << 1) |
                        (BIT(dec_prev_word, 1) << 2) |
                        (BIT(enc_prev_word, 3) << 3) |
                        (BIT(enc_prev_word, 8) << 4) |
                        (BIT(enc_prev_word, 15) << 5);
                    break;
                case 2:
                    k = (BIT(enc_prev_word, 5) << 0) |
                        (BIT(dec_prev_word, 5) << 1) |
                        (BIT(enc_prev_word, 7) << 2) |
                        (BIT(enc_prev_word, 3) << 3) |
                        (BIT(enc_prev_word, 13) << 4) |
                        (BIT(enc_prev_word, 14) << 5);
                    break;
                case 3:
                    k = (BIT(enc_prev_word, 0) << 0) |
                        (BIT(enc_prev_word, 9) << 1) |
                        (BIT(enc_prev_word, 6) << 2) |
                        (BIT(dec_prev_word, 4) << 3) |
                        (BIT(enc_prev_word, 2) << 4) |
                        (BIT(dec_prev_word, 11) << 5);
                    break;
            }
            k ^= param1;
            res = (res & 0xffc0) | ((res + k) & 0x003f);
            res ^= param1;
            switch (type)
            {
                case 0:
                    k = (BIT(enc_word, 9) << 0) |
                        (BIT(res, 2) << 1) |
                        (BIT(enc_word, 5) << 2) |
                        (BIT(res, 5) << 3) |
                        (BIT(res, 4) << 4);
                    break;
                case 1:
                    k = (BIT(dec_prev_word, 2) << 0) |	// always 1
                        (BIT(enc_prev_word, 4) << 1) |
                        (BIT(dec_prev_word, 14) << 2) |
                        (BIT(res, 1) << 3) |
                        (BIT(dec_prev_word, 12) << 4);	// always 0
                    break;
                case 2:
                    k = (BIT(enc_prev_word, 6) << 0) |
                        (BIT(dec_prev_word, 6) << 1) |
                        (BIT(dec_prev_word, 15) << 2) |
                        (BIT(res, 0) << 3) |
                        (BIT(dec_prev_word, 7) << 4);
                    break;
                case 3:
                    k = (BIT(dec_prev_word, 2) << 0) |	// always 1
                        (BIT(dec_prev_word, 9) << 1) |
                        (BIT(enc_prev_word, 5) << 2) |
                        (BIT(dec_prev_word, 1) << 3) |
                        (BIT(enc_prev_word, 10) << 4);
                    break;
            }
            k ^= param1;
            res = (res & 0x003f) |
                    ((res + (k << 6)) & 0x07c0) |
                    ((res + (k << 11)) & 0xf800);
            res ^= (param1 << 6) | (param1 << 11);
            return BITSWAP16(res, 2, 6, 0, 11, 14, 12, 7, 10, 5, 4, 8, 3, 9, 1, 13, 15);
        }
        public static ushort gaelco_decrypt(int offset, int data, int param1, int param2)
        {
            ushort data2;
            int thispc = Cpuexec.activecpu;
            if (lastpc == thispc && offset == lastoffset + 1)
            {
                lastpc = 0;
                data2 = (ushort)decrypt(param1, param2, lastencword, lastdecword, data);
            }
            else
            {
                lastpc = thispc;
                lastoffset = offset;
                lastencword = data;
                data2 = (ushort)decrypt(param1, param2, 0, 0, data);
                lastdecword = data2;
            }
            return data2;
        }
        public static int BIT(int x, int n)
        {
            return (x >> n) & 1;
        }
        public static int BITSWAP8(int val, int B7, int B6, int B5, int B4, int B3, int B2, int B1, int B0)
        {
            return ((BIT(val, B7) << 7) |
                (BIT(val, B6) << 6) |
                (BIT(val, B5) << 5) |
                (BIT(val, B4) << 4) |
                (BIT(val, B3) << 3) |
                (BIT(val, B2) << 2) |
                (BIT(val, B1) << 1) |
                (BIT(val, B0) << 0));
        }
        public static int BITSWAP16(int val, int B15, int B14, int B13, int B12, int B11, int B10, int B9, int B8, int B7, int B6, int B5, int B4, int B3, int B2, int B1, int B0)
        {
            return ((BIT(val, B15) << 15) |
                (BIT(val, B14) << 14) |
                (BIT(val, B13) << 13) |
                (BIT(val, B12) << 12) |
                (BIT(val, B11) << 11) |
                (BIT(val, B10) << 10) |
                (BIT(val, B9) << 9) |
                (BIT(val, B8) << 8) |
                (BIT(val, B7) << 7) |
                (BIT(val, B6) << 6) |
                (BIT(val, B5) << 5) |
                (BIT(val, B4) << 4) |
                (BIT(val, B3) << 3) |
                (BIT(val, B2) << 2) |
                (BIT(val, B1) << 1) |
                (BIT(val, B0) << 0));
        }
    }
}
