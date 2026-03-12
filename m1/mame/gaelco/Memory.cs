using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Gaelco
    {
        public static sbyte sbyte1, sbyte2;
        public static sbyte sbyte1_old, sbyte2_old;
        public static sbyte MReadOpByte_bigkarnk(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
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
        public static sbyte MReadByte_bigkarnk(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    result = (sbyte)Memory.mainrom[address];
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x100000 && address <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(gaelco_videoram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)gaelco_videoram[offset];
                }
            }
            else if (address >= 0x102000 && address <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(gaelco_screen[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)gaelco_screen[offset];
                }
            }
            else if (address >= 0x200000 && address <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x440000 && address <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(gaelco_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)gaelco_spriteram[offset];
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw1;
                }
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw2;
                }
            }
            else if (address >= 0x700004 && address <= 0x700005)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = sbyte1;
                }
            }
            else if (address >= 0x700006 && address <= 0x700007)
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
            else if (address >= 0x700008 && address <= 0x700009)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)bytes;
                }
            }
            else if (address >= 0xff8000 && address <= 0xffffff)
            {
                int offset = address - 0xff8000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short MReadOpWord_bigkarnk(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address + 1 <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
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
        public static short MReadWord_bigkarnk(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x07ffff)
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
            else if (address >= 0x100000 && address + 1 <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                result = (short)gaelco_videoram[offset];
            }
            else if (address >= 0x102000 && address + 1 <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                result = (short)gaelco_screen[offset];
            }
            else if (address >= 0x200000 && address + 1 <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x440000 && address + 1 <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)gaelco_spriteram[offset];
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                result = (short)dsw1;
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                result = (short)dsw2;
            }
            else if (address >= 0x700004 && address + 1 <= 0x700005)
            {
                result = (short)((byte)sbyte1);
            }
            else if (address >= 0x700006 && address + 1 <= 0x700007)
            {
                result = (short)((byte)sbyte2);
            }
            else if (address >= 0x700008 && address + 1 <= 0x700009)
            {
                result = (short)bytes;
            }
            else if (address >= 0xff8000 && address + 1 <= 0xffffff)
            {
                int offset = address - 0xff8000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_bigkarnk(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_bigkarnk(address) * 0x10000 + (ushort)MReadOpWord_bigkarnk(address + 2));
            return result;
        }
        public static int MReadLong_bigkarnk(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_bigkarnk(address) * 0x10000 + (ushort)MReadWord_bigkarnk(address + 2));
            return result;
        }
        public static void MWriteByte_bigkarnk(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vram_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    gaelco_vram_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x102000 && address <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_screen[offset] = (ushort)((value << 8) | (gaelco_screen[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_screen[offset] = (ushort)((gaelco_screen[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x108000 && address <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vregs[offset] = (ushort)((value << 8) | (gaelco_vregs[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_vregs[offset] = (ushort)((gaelco_vregs[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x10800c && address <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_spriteram[offset] = (ushort)((value << 8) | (gaelco_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_spriteram[offset] = (ushort)((gaelco_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x70000e && address <= 0x70000f)
            {
                if (address % 2 == 1)
                {
                    bigkarnk_sound_command_w2((byte)value);
                }
            }
            else if (address >= 0x70000a && address <= 0x70003b)
            {
                int offset = (address - 0x70000a) / 2;
                if (address % 2 == 1)
                {
                    bigkarnk_coin_w2(offset, (byte)value);
                }
            }
            else if (address >= 0xff8000 && address <= 0xffffff)
            {
                int offset = address - 0xff8000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_bigkarnk(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x07ffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)(value >> 8);
                    Memory.mainrom[address + 1] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address + 1 <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                gaelco_vram_w(offset, (ushort)value);
            }
            else if (address >= 0x102000 && address + 1 <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                gaelco_screen[offset] = (ushort)value;
            }
            else if (address >= 0x108000 && address + 1 <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                gaelco_vregs[offset] = (ushort)value;
            }
            else if (address >= 0x10800c && address + 1 <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address + 1 <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                gaelco_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x70000e && address + 1 <= 0x70000f)
            {
                bigkarnk_sound_command_w((ushort)value);
            }
            else if (address >= 0x70000a && address + 1 <= 0x70003b)
            {
                int offset = (address - 0x70000a) / 2;
                bigkarnk_coin_w(offset, (ushort)value);
            }
            else if (address >= 0xff8000 && address + 1 <= 0xffffff)
            {
                int offset = address - 0xff8000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_bigkarnk(int address, int value)
        {
            MWriteWord_bigkarnk(address, (short)(value >> 16));
            MWriteWord_bigkarnk(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_maniacsq(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x0fffff)
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
        public static sbyte MReadByte_maniacsq(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x0fffff)
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
            else if (address >= 0x100000 && address <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(gaelco_videoram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)gaelco_videoram[offset];
                }
            }
            else if (address >= 0x102000 && address <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(gaelco_screen[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)gaelco_screen[offset];
                }
            }
            else if (address >= 0x200000 && address <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x440000 && address <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(gaelco_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)gaelco_spriteram[offset];
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw2;
                }
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw1;
                }
            }
            else if (address >= 0x700004 && address <= 0x700005)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = sbyte1;
                }
            }
            else if (address >= 0x700006 && address <= 0x700007)
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
            else if (address >= 0x70000e && address <= 0x70000f)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)OKI6295.oo1[0].okim6295_status_r();
                }
            }
            else if (address >= 0xff0000 && address <= 0xffffff)
            {
                int offset = address - 0xff0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short MReadOpWord_maniacsq(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address + 1 <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
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
        public static short MReadWord_maniacsq(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address + 1 <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x100000 && address + 1 <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                result = (short)gaelco_videoram[offset];
            }
            else if (address >= 0x102000 && address + 1 <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                result = (short)gaelco_screen[offset];
            }
            else if (address >= 0x200000 && address + 1 <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x440000 && address + 1 <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)gaelco_spriteram[offset];
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                result = (short)dsw2;
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                result = (short)dsw1;
            }
            else if (address >= 0x700004 && address + 1 <= 0x700005)
            {
                result = (short)((byte)sbyte1);
            }
            else if (address >= 0x700006 && address + 1 <= 0x700007)
            {
                result = (short)((byte)sbyte2);
            }
            else if (address >= 0x70000e && address + 1 <= 0x70000f)
            {
                result = (short)OKI6295.oo1[0].okim6295_status_r();
            }
            else if (address >= 0xff0000 && address + 1 <= 0xffffff)
            {
                int offset = address - 0xff0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_maniacsq(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_maniacsq(address) * 0x10000 + (ushort)MReadOpWord_maniacsq(address + 2));
            return result;
        }
        public static int MReadLong_maniacsq(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_maniacsq(address) * 0x10000 + (ushort)MReadWord_maniacsq(address + 2));
            return result;
        }
        public static void MWriteByte_maniacsq(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vram_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    gaelco_vram_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x102000 && address <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_screen[offset] = (ushort)((value << 8) | (gaelco_screen[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_screen[offset] = (ushort)((gaelco_screen[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x108000 && address <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vregs[offset] = (ushort)((value << 8) | (gaelco_vregs[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_vregs[offset] = (ushort)((gaelco_vregs[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x10800c && address <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_spriteram[offset] = (ushort)((value << 8) | (gaelco_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_spriteram[offset] = (ushort)((gaelco_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x70000c && address <= 0x70000d)
            {
                if (address % 2 == 1)
                {
                    OKIM6295_bankswitch_w2((byte)value);
                }
            }
            else if (address >= 0x70000e && address <= 0x70000f)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[0].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0xff0000 && address <= 0xffffff)
            {
                int offset = address - 0xff0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_maniacsq(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x0fffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)(value >> 8);
                    Memory.mainrom[address + 1] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address + 1 <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                gaelco_vram_w(offset, (ushort)value);
            }
            else if (address >= 0x102000 && address + 1 <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                gaelco_screen[offset] = (ushort)value;
            }
            else if (address >= 0x108000 && address + 1 <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                gaelco_vregs[offset] = (ushort)value;
            }
            else if (address >= 0x10800c && address + 1 <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address + 1 <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                gaelco_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x70000c && address + 1 <= 0x70000d)
            {
                OKIM6295_bankswitch_w((ushort)value);
            }
            else if (address >= 0x70000e && address + 1 <= 0x70000f)
            {
                OKI6295.oo1[0].okim6295_data_w((byte)value);
            }
            else if (address >= 0xff0000 && address + 1 <= 0xffffff)
            {
                int offset = address - 0xff0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_maniacsq(int address, int value)
        {
            MWriteWord_maniacsq(address, (short)(value >> 16));
            MWriteWord_maniacsq(address + 2, (short)value);
        }
        public static void MWriteByte_squash(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vram_encrypted_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    gaelco_vram_encrypted_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x102000 && address <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_encrypted_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    gaelco_encrypted_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x108000 && address <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vregs[offset] = (ushort)((value << 8) | (gaelco_vregs[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_vregs[offset] = (ushort)((gaelco_vregs[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x10800c && address <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_spriteram[offset] = (ushort)((value << 8) | (gaelco_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_spriteram[offset] = (ushort)((gaelco_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x70000c && address <= 0x70000d)
            {
                if (address % 2 == 1)
                {
                    OKIM6295_bankswitch_w2((byte)value);
                }
            }
            else if (address >= 0x70000e && address <= 0x70000f)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[0].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0xff0000 && address <= 0xffffff)
            {
                int offset = address - 0xff0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_squash(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x0fffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)(value >> 8);
                    Memory.mainrom[address + 1] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address + 1 <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                gaelco_vram_encrypted_w(offset, (ushort)value);
            }
            else if (address >= 0x102000 && address + 1 <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                gaelco_encrypted_w(offset, (ushort)value);
            }
            else if (address >= 0x108000 && address + 1 <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                gaelco_vregs[offset] = (ushort)value;
            }
            else if (address >= 0x10800c && address + 1 <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address + 1 <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                gaelco_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x70000c && address + 1 <= 0x70000d)
            {
                OKIM6295_bankswitch_w((ushort)value);
            }
            else if (address >= 0x70000e && address + 1 <= 0x70000f)
            {
                OKI6295.oo1[0].okim6295_data_w((byte)value);
            }
            else if (address >= 0xff0000 && address + 1 <= 0xffffff)
            {
                int offset = address - 0xff0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_squash(int address, int value)
        {
            MWriteWord_squash(address, (short)(value >> 16));
            MWriteWord_squash(address + 2, (short)value);
        }
        public static void MWriteByte_thoop(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                if (address % 2 == 0)
                {
                    thoop_vram_encrypted_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    thoop_vram_encrypted_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x102000 && address <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                if (address % 2 == 0)
                {
                    thoop_encrypted_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    thoop_encrypted_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x108000 && address <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_vregs[offset] = (ushort)((value << 8) | (gaelco_vregs[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_vregs[offset] = (ushort)((gaelco_vregs[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x10800c && address <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    gaelco_spriteram[offset] = (ushort)((value << 8) | (gaelco_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    gaelco_spriteram[offset] = (ushort)((gaelco_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x70000c && address <= 0x70000d)
            {
                if (address % 2 == 1)
                {
                    OKIM6295_bankswitch_w2((byte)value);
                }
            }
            else if (address >= 0x70000e && address <= 0x70000f)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[0].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0xff0000 && address <= 0xffffff)
            {
                int offset = address - 0xff0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_thoop(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x0fffff)
            {
                if (address + 1 < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)(value >> 8);
                    Memory.mainrom[address + 1] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address + 1 <= 0x101fff)
            {
                int offset = (address - 0x100000) / 2;
                thoop_vram_encrypted_w(offset, (ushort)value);
            }
            else if (address >= 0x102000 && address + 1 <= 0x103fff)
            {
                int offset = (address - 0x102000) / 2;
                thoop_encrypted_w(offset, (ushort)value);
            }
            else if (address >= 0x108000 && address + 1 <= 0x108007)
            {
                int offset = (address - 0x108000) / 2;
                gaelco_vregs[offset] = (ushort)value;
            }
            else if (address >= 0x10800c && address + 1 <= 0x10800d)
            {
                irqack_w();
            }
            else if (address >= 0x200000 && address + 1 <= 0x2007ff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_xBBBBBGGGGGRRRRR_word_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x440fff)
            {
                int offset = (address - 0x440000) / 2;
                gaelco_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x70000c && address + 1 <= 0x70000d)
            {
                OKIM6295_bankswitch_w((ushort)value);
            }
            else if (address >= 0x70000e && address + 1 <= 0x70000f)
            {
                OKI6295.oo1[0].okim6295_data_w((byte)value);
            }
            else if (address >= 0xff0000 && address + 1 <= 0xffffff)
            {
                int offset = address - 0xff0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_thoop(int address, int value)
        {
            MWriteWord_thoop(address, (short)(value >> 16));
            MWriteWord_thoop(address + 2, (short)value);
        }
        public static byte M6809ReadOp_bigkarnk(ushort address)
        {
            byte result = 0;
            if (address >= 0x0c00 && address <= 0xffff)
            {
                result = Memory.audiorom[address];
            }
            return result;
        }
        public static byte M6809ReadByte_bigkarnk(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x07ff)
            {
                result = Memory.audioram[address];
            }
            else if (address >= 0x0800 && address <= 0x0801)
            {
                result = (byte)OKI6295.oo1[0].okim6295_status_r();
            }
            else if (address == 0x0a00)
            {
                result = FMOpl.ym3812_read(0);
            }
            else if (address == 0x0b00)
            {
                result = (byte)Sound.soundlatch_r();
            }
            else if (address >= 0x0c00 && address <= 0xffff)
            {
                result = Memory.audiorom[address];
            }
            return result;
        }
        public static void M6809WriteByte_bigkarnk(ushort address, byte data)
        {
            if (address >= 0x0000 && address <= 0x07ff)
            {
                Memory.audioram[address] = data;
            }
            else if (address == 0x0800)
            {
                OKI6295.oo1[0].okim6295_data_w(data);
            }
            else if (address == 0x0a00)
            {
                FMOpl.ym3812_write(0, data);
            }
            else if (address == 0x0a01)
            {
                FMOpl.ym3812_write(1, data);
            }
            else if (address >= 0x0c00 && address <= 0xffff)
            {
                Memory.audiorom[address] = data;
            }
        }
    }
}
