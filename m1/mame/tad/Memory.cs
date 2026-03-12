using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Tad
    {
        public static short short1;
        public static byte bytec;
        public static sbyte sbyte2;
        public static short short1_old;
        public static byte bytec_old;
        public static sbyte sbyte2_old;
        public static sbyte MReadOpByte_toki(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x05ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    result = (sbyte)(Memory.mainrom[address]);
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }
        public static sbyte MReadByte_toki(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x05ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    result = (sbyte)(Memory.mainrom[address]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x060000 && address <= 0x06d7ff)
            {
                int offset = address - 0x060000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x06d800 && address <= 0x06dfff)
            {
                int offset = (address - 0x06d800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.spriteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.spriteram16[offset];
                }
            }
            else if (address >= 0x06e000 && address <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x06e800 && address <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(toki_background1_videoram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)toki_background1_videoram16[offset];
                }
            }
            else if (address >= 0x06f000 && address <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(toki_background2_videoram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)toki_background2_videoram16[offset];
                }
            }
            else if (address >= 0x06f800 && address <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.videoram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.videoram16[offset];
                }
            }
            else if (address >= 0x080000 && address <= 0x08000d)
            {
                int offset = (address - 0x080000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(seibu_main_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)seibu_main_word_r(offset);
                }
            }
            else if (address >= 0x0c0000 && address <= 0x0c0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(dsw0 >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw0;
                }
            }
            else if (address >= 0x0c0002 && address <= 0x0c0003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(short1 >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)short1;
                }
            }
            else if (address >= 0x0c0004 && address <= 0x0c0005)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = sbyte2;
                }
            }
            return result;
        }
        public static short MReadOpWord_toki(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x05ffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }
        public static short MReadWord_toki(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x05ffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    result = (short)(Memory.mainrom[address]*0x100+Memory.mainrom[address+1]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x060000 && address + 1 <= 0x06d7ff)
            {
                int offset = address - 0x060000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x06d800 && address + 1 <= 0x06dfff)
            {
                int offset = (address - 0x06d800) / 2;
                result = (short)Generic.spriteram16[offset];
            }
            else if (address >= 0x06e000 && address + 1 <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x06e800 && address + 1 <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                result = (short)toki_background1_videoram16[offset];
            }
            else if (address >= 0x06f000 && address + 1 <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                result = (short)toki_background2_videoram16[offset];
            }
            else if (address >= 0x06f800 && address + 1 <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                result = (short)Generic.videoram16[offset];
            }
            else if (address >= 0x080000 && address + 1 <= 0x08000d)
            {
                int offset = (address - 0x080000) / 2;
                result = (short)seibu_main_word_r(offset);
            }
            else if (address >= 0x0c0000 && address + 1 <= 0x0c0001)
            {
                result = (short)dsw0;
            }
            else if (address >= 0x0c0002 && address + 1 <= 0x0c0003)
            {
                result = short1;
            }
            else if (address >= 0x0c0004 && address + 1 <= 0x0c0005)
            {
                /*if (Video.screenstate.frame_number >= 10 && Video.screenstate.frame_number <= 11)
                {
                    result = (short)0xf7;
                }
                else*/
                {
                    result = (short)((byte)sbyte2);
                }
            }
            return result;
        }
        public static int MReadOpLong_toki(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_toki(address) * 0x10000 + (ushort)MReadOpWord_toki(address + 2));
            return result;
        }
        public static int MReadLong_toki(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_toki(address) * 0x10000 + (ushort)MReadWord_toki(address + 2));
            return result;
        }
        public static void MWriteByte_toki(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x060000 && address <= 0x06d7ff)
            {
                int offset = address - 0x060000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x06d800 && address <= 0x06dfff)
            {
                int offset = (address - 0x06d800) / 2;
                if (address % 2 == 0)
                {
                    Generic.spriteram16[offset] = (ushort)((value << 8) | (Generic.spriteram16[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    Generic.spriteram16[offset] = (ushort)((Generic.spriteram16[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x06e000 && address <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_xxxxBBBBGGGGRRRR_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_xxxxBBBBGGGGRRRR_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x06e800 && address <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                if (address % 2 == 0)
                {
                    toki_background1_videoram16_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_background1_videoram16_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x06f000 && address <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                if (address % 2 == 0)
                {
                    toki_background2_videoram16_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_background2_videoram16_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x06f800 && address <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                if (address % 2 == 0)
                {
                    toki_foreground_videoram16_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_foreground_videoram16_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x080000 && address <= 0x08000d)
            {
                int offset = (address - 0x080000) / 2;
                if (address % 2 == 0)
                {

                }
                else if (address % 2 == 1)
                {
                    seibu_main_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x0a0000 && address <= 0x0a005f)
            {
                int offset = (address - 0x0a0000) / 2;
                if (address % 2 == 0)
                {
                    toki_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_control_w2(offset, (byte)value);
                }
            }
        }
        public static void MWriteWord_toki(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x060000 && address + 1 <= 0x06d7ff)
            {
                int offset = address - 0x060000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x06d800 && address + 1 <= 0x06dfff)
            {
                int offset = (address - 0x06d800) / 2;
                Generic.spriteram16[offset] = (ushort)value;
            }
            else if (address >= 0x06e000 && address + 1 <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                Generic.paletteram16_xxxxBBBBGGGGRRRR_word_w(offset, (ushort)value);
            }
            else if (address >= 0x06e800 && address + 1 <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                toki_background1_videoram16_w(offset, (ushort)value);
            }
            else if (address >= 0x06f000 && address + 1 <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                toki_background2_videoram16_w(offset, (ushort)value);
            }
            else if (address >= 0x06f800 && address + 1 <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                toki_foreground_videoram16_w(offset, (ushort)value);
            }
            else if (address >= 0x080000 && address + 1 <= 0x08000d)
            {
                int offset = (address - 0x080000) / 2;
                seibu_main_word_w(offset, (ushort)value);
            }
            else if (address >= 0x0a0000 && address + 1 <= 0x0a005f)
            {
                int offset = (address - 0x0a0000) / 2;
                toki_control_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_toki(int address, int value)
        {
            MWriteWord_toki(address, (short)(value >> 16));
            MWriteWord_toki(address + 2, (short)value);
        }
        public static sbyte MReadByte_tokib(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x05ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    result = (sbyte)(Memory.mainrom[address]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x060000 && address <= 0x06dfff)
            {
                int offset = address - 0x060000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x06e000 && address <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x06e800 && address <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(toki_background1_videoram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)toki_background1_videoram16[offset];
                }
            }
            else if (address >= 0x06f000 && address <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(toki_background2_videoram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)toki_background2_videoram16[offset];
                }
            }
            else if (address >= 0x06f800 && address <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.videoram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.videoram16[offset];
                }
            }
            else if (address >= 0x07180e && address <= 0x071e45)
            {
                int offset = (address - 0x07180e) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.spriteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.spriteram16[offset];
                }
            }
            else if (address >= 0x072000 && address <= 0x072001)
            {
                result = (sbyte)Generic.watchdog_reset16_r();
            }
            else if (address >= 0x0c0000 && address <= 0x0c0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(dsw0 >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw0;
                }
            }
            else if (address >= 0x0c0002 && address <= 0x0c0003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(short1 >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)short1;
                }
            }
            else if (address >= 0x0c0004 && address <= 0x0c0005)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = sbyte2;
                }
            }
            else if (address >= 0x0c000e && address <= 0x0c000f)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(pip16_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)pip16_r();
                }
            }
            return result;
        }
        public static short MReadWord_tokib(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x05ffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x060000 && address + 1 <= 0x06dfff)
            {
                int offset = address - 0x060000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x06e000 && address + 1 <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x06e800 && address + 1 <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                result = (short)toki_background1_videoram16[offset];
            }
            else if (address >= 0x06f000 && address + 1 <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                result = (short)toki_background2_videoram16[offset];
            }
            else if (address >= 0x06f800 && address + 1 <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                result = (short)Generic.videoram16[offset];
            }
            else if (address >= 0x07180e && address+ 1 <= 0x071e45)
            {
                int offset = (address - 0x07180e) / 2;
                result = (short)Generic.spriteram16[offset];
            }
            else if (address >= 0x072000 && address+ 1 <= 0x072001)
            {
                result = (short)Generic.watchdog_reset16_r();
            }
            else if (address >= 0x0c0000 && address + 1 <= 0x0c0001)
            {
                result = (short)dsw0;
            }
            else if (address >= 0x0c0002 && address+ 1 <= 0x0c0003)
            {
                result = short1;
            }
            else if (address >= 0x0c0004 && address + 1 <= 0x0c0005)
            {
                result = (short)((byte)sbyte2);
            }
            else if (address >= 0x0c000e && address <= 0x0c000f)
            {
                result = (short)pip16_r();
            }
            return result;
        }
        public static int MReadLong_tokib(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_tokib(address) * 0x10000 + (ushort)MReadWord_tokib(address + 2));
            return result;
        }
        public static void MWriteByte_tokib(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x060000 && address <= 0x06dfff)
            {
                int offset = address - 0x060000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x06e000 && address <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_xxxxBBBBGGGGRRRR_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_xxxxBBBBGGGGRRRR_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x06e800 && address <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                if (address % 2 == 0)
                {
                    toki_background1_videoram16_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_background1_videoram16_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x06f000 && address <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                if (address % 2 == 0)
                {
                    toki_background2_videoram16_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_background2_videoram16_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x06f800 && address <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                if (address % 2 == 0)
                {
                    toki_foreground_videoram16_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    toki_foreground_videoram16_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x071000 && address <= 0x071001)
            {

            }
            else if (address >= 0x071804 && address <= 0x071807)
            {

            }
            else if (address >= 0x07180e && address <= 0x071e45)
            {
                int offset = (address - 0x07180e) / 2;
                if (address % 2 == 0)
                {
                    Generic.spriteram16[offset] = (ushort)((value << 8) | (Generic.spriteram16[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    Generic.spriteram16[offset] = (ushort)((Generic.spriteram16[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x075000 && address <= 0x075001)
            {
                tokib_soundcommand16_w1((byte)value);
            }
            else if (address >= 0x075004 && address <= 0x07500b)
            {
                int offset = (address - 0x075004) / 2;
                if (address % 2 == 0)
                {
                    toki_scrollram16[offset] = (ushort)((value << 8) | (toki_scrollram16[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    toki_scrollram16[offset] = (ushort)((toki_scrollram16[offset] & 0xff00) | (byte)value);
                }
            }
        }
        public static void MWriteWord_tokib(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x060000 && address + 1 <= 0x06dfff)
            {
                int offset = address - 0x060000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x06e000 && address + 1 <= 0x06e7ff)
            {
                int offset = (address - 0x06e000) / 2;
                Generic.paletteram16_xxxxBBBBGGGGRRRR_word_w(offset, (ushort)value);
            }
            else if (address >= 0x06e800 && address + 1 <= 0x06efff)
            {
                int offset = (address - 0x06e800) / 2;
                toki_background1_videoram16_w(offset, (ushort)value);
            }
            else if (address >= 0x06f000 && address + 1 <= 0x06f7ff)
            {
                int offset = (address - 0x06f000) / 2;
                toki_background2_videoram16_w(offset, (ushort)value);
            }
            else if (address >= 0x06f800 && address + 1 <= 0x06ffff)
            {
                int offset = (address - 0x06f800) / 2;
                toki_foreground_videoram16_w(offset, (ushort)value);
            }
            else if (address >= 0x071000 && address+ 1 <= 0x071001)
            {

            }
            else if (address >= 0x071804 && address+ 1 <= 0x071807)
            {

            }
            else if (address >= 0x07180e && address + 1 <= 0x071e45)
            {
                int offset = (address - 0x07180e) / 2;
                Generic.spriteram16[offset] = (ushort)value;
            }
            else if (address >= 0x075000 && address+1 <= 0x075001)
            {
                tokib_soundcommand16_w((ushort)value);
            }
            else if (address >= 0x075004 && address <= 0x07500b)
            {
                int offset = (address - 0x075004) / 2;
                toki_scrollram16[offset] = (ushort)value;
            }
        }
        public static void MWriteLong_tokib(int address, int value)
        {
            MWriteWord_tokib(address, (short)(value >> 16));
            MWriteWord_tokib(address + 2, (short)value);
        }
        public static byte ZReadOp_seibu(ushort address)
        {
            byte result = 0;
            if (address <= 0x1fff)
            {
                result = audioromop[address];
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                int offset = address - 0x8000;
                result = audioromop[basebanksnd + offset];
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public static byte ZReadMemory_seibu(ushort address)
        {
            byte result = 0;
            if (address <= 0x1fff)
            {
                result = Memory.audiorom[address];
            }
            else if (address >= 0x2000 && address <= 0x27ff)
            {
                int offset = address - 0x2000;
                result = Memory.audioram[offset];
            }
            else if (address == 0x4008)
            {
                result = YM3812.ym3812_status_port_0_r();
            }
            else if (address >= 0x4010 && address <= 0x4011)
            {
                int offset = address - 0x4010;
                result = seibu_soundlatch_r(offset);
            }
            else if (address == 0x4012)
            {
                result = seibu_main_data_pending_r();
            }
            else if (address == 0x4013)
            {
                /*if (Video.screenstate.frame_number >= 0x30 && Video.screenstate.frame_number <= 0x35)
                {
                    result = 0x01;
                }
                else*/
                {
                    result = bytec;
                }
            }
            else if (address == 0x6000)
            {
                result = (byte)OKI6295.oo1[0].okim6295_status_r();
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                int offset = address - 0x8000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public static void ZWriteMemory_seibu(ushort address, byte value)
        {
            if (address >= 0x2000 && address <= 0x27ff)
            {
                int offset = address - 0x2000;
                Memory.audioram[offset] = value;
            }
            else if (address == 0x4000)
            {
                seibu_pending_w();
            }
            else if (address == 0x4001)
            {
                seibu_irq_clear_w();
            }
            else if (address == 0x4002)
            {
                seibu_rst10_ack_w();
            }
            else if (address == 0x4003)
            {
                seibu_rst18_ack_w();
            }
            else if (address == 0x4007)
            {
                seibu_bank_w(value);
            }
            else if (address == 0x4008)
            {
                YM3812.ym3812_control_port_0_w(value);
            }
            else if (address == 0x4009)
            {
                YM3812.ym3812_write_port_0_w(value);
            }
            else if (address >= 0x4018 && address <= 0x4019)
            {
                int offset = address - 0x4018;
                seibu_main_data_w(offset, value);
            }
            else if (address == 0x401b)
            {
                seibu_coin_w();
            }
            else if (address == 0x6000)
            {
                OKI6295.oo1[0].okim6295_data_w(value);
            }
        }
        public static byte ZReadOp_tokib(ushort address)
        {
            byte result = 0;
            if (address <= 0x7fff)
            {
                result = Memory.audiorom[address];
            }
            else if (address >= 0x8000 && address <= 0xbfff)
            {
                int offset = address - 0x8000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public static byte ZReadMemory_tokib(ushort address)
        {
            byte result = 0;
            if (address <= 0x7fff)
            {
                result = Memory.audiorom[address];
            }
            else if (address >= 0x8000 && address <= 0xbfff)
            {
                int offset = address - 0x8000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else if (address == 0xec00)
            {
                result = YM3812.ym3812_status_port_0_r();
            }
            else if (address >= 0xf000 && address <= 0xf7ff)
            {
                int offset = address - 0xf000;
                result = Memory.audioram[offset];
            }
            else if (address == 0xf800)
            {
                result = (byte)Sound.soundlatch_r();
            }
            return result;
        }
        public static void ZWriteMemory_tokib(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0xbfff)
            {
                Memory.audiorom[address] = value;
            }
            else if (address == 0xe000)
            {
                toki_adpcm_control_w(value);
            }
            else if (address == 0xe400)
            {
                toki_adpcm_data_w(value);
            }
            else if (address == 0xec00)
            {
                YM3812.ym3812_control_port_0_w(value);
            }
            else if (address == 0xec01)
            {
                YM3812.ym3812_write_port_0_w(value);
            }
            else if (address == 0xec08)
            {
                YM3812.ym3812_control_port_0_w(value);
            }
            else if (address == 0xec09)
            {
                YM3812.ym3812_write_port_0_w(value);
            }
            else if (address >= 0xf000 && address <= 0xf7ff)
            {
                int offset = address - 0xf000;
                Memory.audioram[offset] = value;
            }
        }
        public static byte ZReadHardware(ushort address)
        {
            return 0;
        }
        public static void ZWriteHardware(ushort address, byte value)
        {

        }
    }
}
