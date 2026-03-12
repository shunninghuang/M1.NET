using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Megasys1
    {
        public static short shorts, short1, short2;
        public static short shorts_old, short1_old, short2_old;
        public static sbyte M0ReadOpByte_Z(int address)
        {
            address &= 0xfffff;
            sbyte result = 0;
            if (address <= 0x05ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x0f0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte M0ReadByte_Z(int address)
        {
            address &= 0xfffff;
            sbyte result = 0;
            if (address <= 0x05ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x080000 && address <= 0x080001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(shorts >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)shorts;
                }
            }
            else if (address >= 0x080002 && address <= 0x080003)
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
            else if (address >= 0x080004 && address <= 0x080005)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(short2 >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)short2;
                }
            }
            else if (address >= 0x080006 && address <= 0x080007)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)dsw1;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw2;
                }
            }
            else if (address >= 0x084200 && address <= 0x084201)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[0];
                }
            }
            else if (address >= 0x084202 && address <= 0x084203)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[0];
                }
            }
            else if (address >= 0x084204 && address <= 0x084205)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[0];
                }
            }
            else if (address >= 0x084208 && address <= 0x084209)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[1];
                }
            }
            else if (address >= 0x08420a && address <= 0x08420b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[1];
                }
            }
            else if (address >= 0x08420c && address <= 0x08420d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[1];
                }
            }
            else if (address >= 0x084300 && address <= 0x084301)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_screen_flag >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_screen_flag;
                }
            }
            else if (address >= 0x088000 && address <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x08e000 && address <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_objectram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_objectram[offset];
                }
            }
            else if (address >= 0x090000 && address <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[0][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[0][offset];
                }
            }
            else if (address >= 0x094000 && address <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[1][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[1][offset];
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short M0ReadOpWord_Z(int address)
        {
            address &= 0xfffff;
            short result = 0;
            if (address + 1 <= 0x05ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short M0ReadWord_Z(int address)
        {
            address &= 0xfffff;
            short result = 0;
            if (address + 1 <= 0x05ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x080000 && address + 1 <= 0x080001)
            {
                result = shorts;
            }
            else if (address >= 0x080002 && address + 1 <= 0x080003)
            {
                result = short1;
            }
            else if (address >= 0x080004 && address + 1 <= 0x080005)
            {
                result = short2;
            }
            else if (address >= 0x080006 && address + 1 <= 0x080007)
            {
                result = (short)dsw_r();
            }
            else if (address >= 0x084200 && address + 1 <= 0x084201)
            {
                result = (short)megasys1_scrollx[0];
            }
            else if (address >= 0x084202 && address + 1 <= 0x084203)
            {
                result = (short)megasys1_scrolly[0];
            }
            else if (address >= 0x084204 && address + 1 <= 0x084205)
            {
                result = (short)megasys1_scroll_flag[0];
            }
            else if (address >= 0x084208 && address + 1 <= 0x084209)
            {
                result = (short)megasys1_scrollx[1];
            }
            else if (address >= 0x08420a && address + 1 <= 0x08420b)
            {
                result = (short)megasys1_scrolly[1];
            }
            else if (address >= 0x08420c && address + 1 <= 0x08420d)
            {
                result = (short)megasys1_scroll_flag[1];
            }
            else if (address >= 0x084300 && address + 1 <= 0x084301)
            {
                result = (short)megasys1_screen_flag;
            }
            else if (address >= 0x088000 && address + 1 <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x08e000 && address + 1 <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                result = (short)megasys1_objectram[offset];
            }
            else if (address >= 0x090000 && address + 1 <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                result = (short)megasys1_scrollram[0][offset];
            }
            else if (address >= 0x094000 && address + 1 <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                result = (short)megasys1_scrollram[1][offset];
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int M0ReadOpLong_Z(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadOpWord_Z(address) * 0x10000 + (ushort)M0ReadOpWord_Z(address + 2));
            return result;
        }
        public static int M0ReadLong_Z(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadWord_Z(address) * 0x10000 + (ushort)M0ReadWord_Z(address + 2));
            return result;
        }
        public static void M0WriteByte_Z(int address, sbyte value)
        {
            address &= 0xfffff;
            if (address >= 0x084200 && address <= 0x084201)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx0_w2((byte)value);
                }
            }
            else if (address >= 0x084202 && address <= 0x084203)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly0_w2((byte)value);
                }
            }
            else if (address >= 0x084204 && address <= 0x084205)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(0, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(0, (byte)value);
                }
            }
            else if (address >= 0x084208 && address <= 0x084209)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx1_w2((byte)value);
                }
            }
            else if (address >= 0x08420a && address <= 0x08420b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly1_w2((byte)value);
                }
            }
            else if (address >= 0x08420c && address <= 0x08420d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(1, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(1, (byte)value);
                }
            }
            else if (address >= 0x084300 && address <= 0x084301)
            {
                if (address % 2 == 0)
                {
                    screen_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    screen_flag_w2((byte)value);
                }
            }
            else if (address >= 0x084308 && address <= 0x084309)
            {
                if (address % 2 == 0)
                {

                }
                else if (address % 2 == 1)
                {
                    soundlatch_z_w2((byte)value);
                }
            }
            else if (address >= 0x088000 && address <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x08e000 && address <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                if (address % 2 == 0)
                {
                    megasys1_objectram[offset] = (ushort)((value << 8) | (megasys1_objectram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    megasys1_objectram[offset] = (ushort)((megasys1_objectram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x090000 && address <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(0, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(0, offset, (byte)value);
                }
            }
            else if (address >= 0x094000 && address <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(1, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(1, offset, (byte)value);
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void M0WriteWord_Z(int address, short value)
        {
            address &= 0xfffff;
            if (address >= 0x084200 && address + 1 <= 0x084201)
            {
                megasys1_scrollx0_w((ushort)value);
            }
            else if (address >= 0x084202 && address + 1 <= 0x084203)
            {
                megasys1_scrolly0_w((ushort)value);
            }
            else if (address >= 0x084204 && address + 1 <= 0x084205)
            {
                megasys1_set_vreg_flag(0, (ushort)value);
            }
            else if (address >= 0x084208 && address + 1 <= 0x084209)
            {
                megasys1_scrollx1_w((ushort)value);
            }
            else if (address >= 0x08420a && address + 1 <= 0x08420b)
            {
                megasys1_scrolly1_w((ushort)value);
            }
            else if (address >= 0x08420c && address + 1 <= 0x08420d)
            {
                megasys1_set_vreg_flag(1, (ushort)value);
            }
            else if (address >= 0x084300 && address + 1 <= 0x084301)
            {
                screen_flag_w((ushort)value);
            }
            else if (address >= 0x084308 && address + 1 <= 0x084309)
            {
                soundlatch_z_w((ushort)value);
            }
            else if (address >= 0x088000 && address + 1 <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x08e000 && address + 1 <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                megasys1_objectram[offset] = (ushort)value;
            }
            else if (address >= 0x090000 && address + 1 <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                scrollram_w(0, offset, (ushort)value);
            }
            else if (address >= 0x094000 && address + 1 <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                scrollram_w(1, offset, (ushort)value);
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void M0WriteLong_Z(int address, int value)
        {
            M0WriteWord_Z(address, (short)(value >> 16));
            M0WriteWord_Z(address + 2, (short)value);
        }
        public static sbyte M0ReadByte_A(int address)
        {
            address &= 0xfffff;
            sbyte result = 0;
            if (address <= 0x05ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x080000 && address <= 0x080001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(shorts >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)shorts;
                }
            }
            else if (address >= 0x080002 && address <= 0x080003)
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
            else if (address >= 0x080004 && address <= 0x080005)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(short2 >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)short2;
                }
            }
            else if (address >= 0x080006 && address <= 0x080007)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)dsw1;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw2;
                }
            }
            else if (address >= 0x080008 && address <= 0x080009)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Sound.soundlatch2_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Sound.soundlatch2_r();
                }
            }
            else if (address >= 0x084000 && address <= 0x084001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_active_layers >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_active_layers;
                }
            }
            else if (address >= 0x084008 && address <= 0x084009)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[2];
                }
            }
            else if (address >= 0x08400a && address <= 0x08400b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[2];
                }
            }
            else if (address >= 0x08400c && address <= 0x08400d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[2];
                }
            }
            else if (address >= 0x084100 && address <= 0x084101)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(sprite_flag_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)sprite_flag_r();
                }
            }

            else if (address >= 0x084200 && address <= 0x084201)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[0];
                }
            }
            else if (address >= 0x084202 && address <= 0x084203)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[0];
                }
            }
            else if (address >= 0x084204 && address <= 0x084205)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[0];
                }
            }
            else if (address >= 0x084208 && address <= 0x084209)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[1];
                }
            }
            else if (address >= 0x08420a && address <= 0x08420b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[1];
                }
            }
            else if (address >= 0x08420c && address <= 0x08420d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[1];
                }
            }
            else if (address >= 0x084300 && address <= 0x084301)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_screen_flag >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_screen_flag;
                }
            }
            else if (address >= 0x088000 && address <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x08e000 && address <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_objectram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_objectram[offset];
                }
            }
            else if (address >= 0x090000 && address <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[0][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[0][offset];
                }
            }
            else if (address >= 0x094000 && address <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[1][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[1][offset];
                }
            }
            else if (address >= 0x098000 && address <= 0x09bfff)
            {
                int offset = (address - 0x098000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[2][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[2][offset];
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short M0ReadWord_A(int address)
        {
            address &= 0xfffff;
            short result = 0;
            if (address + 1 <= 0x05ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x080000 && address + 1 <= 0x080001)
            {
                result = shorts;
            }
            else if (address >= 0x080002 && address + 1 <= 0x080003)
            {
                result = short1;
            }
            else if (address >= 0x080004 && address + 1 <= 0x080005)
            {
                result = short2;
            }
            else if (address >= 0x080006 && address + 1 <= 0x080007)
            {
                result = (short)dsw_r();
            }
            else if (address >= 0x080008 && address + 1 <= 0x080009)
            {
                result = (short)Sound.soundlatch2_r();
            }
            else if (address >= 0x084000 && address + 1 <= 0x084001)
            {
                result = (short)megasys1_active_layers;
            }
            else if (address >= 0x084008 && address + 1 <= 0x084009)
            {
                result = (short)megasys1_scrollx[2];
            }
            else if (address >= 0x08400a && address + 1 <= 0x08400b)
            {
                result = (short)megasys1_scrolly[2];
            }
            else if (address >= 0x08400c && address + 1 <= 0x08400d)
            {
                result = (short)megasys1_scroll_flag[2];
            }
            else if (address >= 0x084100 && address + 1 <= 0x084101)
            {
                result = (short)sprite_flag_r();
            }
            else if (address >= 0x084200 && address + 1 <= 0x084201)
            {
                result = (short)megasys1_scrollx[0];
            }
            else if (address >= 0x084202 && address + 1 <= 0x084203)
            {
                result = (short)megasys1_scrolly[0];
            }
            else if (address >= 0x084204 && address + 1 <= 0x084205)
            {
                result = (short)megasys1_scroll_flag[0];
            }
            else if (address >= 0x084208 && address + 1 <= 0x084209)
            {
                result = (short)megasys1_scrollx[1];
            }
            else if (address >= 0x08420a && address + 1 <= 0x08420b)
            {
                result = (short)megasys1_scrolly[1];
            }
            else if (address >= 0x08420c && address + 1 <= 0x08420d)
            {
                result = (short)megasys1_scroll_flag[1];
            }
            else if (address >= 0x084300 && address + 1 <= 0x084301)
            {
                result = (short)megasys1_screen_flag;
            }
            else if (address >= 0x088000 && address + 1 <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x08e000 && address + 1 <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                result = (short)megasys1_objectram[offset];
            }
            else if (address >= 0x090000 && address + 1 <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                result = (short)megasys1_scrollram[0][offset];
            }
            else if (address >= 0x094000 && address + 1 <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                result = (short)megasys1_scrollram[1][offset];
            }
            else if (address >= 0x098000 && address + 1 <= 0x09bfff)
            {
                int offset = (address - 0x098000) / 2;
                result = (short)megasys1_scrollram[2][offset];
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int M0ReadLong_A(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadWord_A(address) * 0x10000 + (ushort)M0ReadWord_A(address + 2));
            return result;
        }
        public static void M0WriteByte_A(int address, sbyte value)
        {
            address &= 0xfffff;
            if (address >= 0x084000 && address <= 0x084001)
            {
                if (address % 2 == 0)
                {
                    active_layers_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    active_layers_w2((byte)value);
                }
            }
            else if (address >= 0x084008 && address <= 0x084009)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx2_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx2_w2((byte)value);
                }
            }
            else if (address >= 0x08400a && address <= 0x08400b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly2_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly2_w2((byte)value);
                }
            }
            else if (address >= 0x08400c && address <= 0x08400d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(2, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(2, (byte)value);
                }
            }
            else if (address >= 0x084100 && address <= 0x084101)
            {
                if (address % 2 == 0)
                {
                    sprite_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    sprite_flag_w2((byte)value);
                }
            }
            else if (address >= 0x084200 && address <= 0x084201)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx0_w2((byte)value);
                }
            }
            else if (address >= 0x084202 && address <= 0x084203)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly0_w2((byte)value);
                }
            }
            else if (address >= 0x084204 && address <= 0x084205)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(0, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(0, (byte)value);
                }
            }
            else if (address >= 0x084208 && address <= 0x084209)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx1_w2((byte)value);
                }
            }
            else if (address >= 0x08420a && address <= 0x08420b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly1_w2((byte)value);
                }
            }
            else if (address >= 0x08420c && address <= 0x08420d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(1, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(1, (byte)value);
                }
            }
            else if (address >= 0x084300 && address <= 0x084301)
            {
                if (address % 2 == 0)
                {
                    screen_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    screen_flag_w2((byte)value);
                }
            }
            else if (address >= 0x084308 && address <= 0x084309)
            {
                if (address % 2 == 0)
                {

                }
                else if (address % 2 == 1)
                {
                    megasys1_soundlatch_w2((byte)value);
                }
            }
            else if (address >= 0x088000 && address <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x08e000 && address <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                if (address % 2 == 0)
                {
                    megasys1_objectram[offset] = (ushort)((value << 8) | (megasys1_objectram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    megasys1_objectram[offset] = (ushort)((megasys1_objectram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x090000 && address <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(0, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(0, offset, (byte)value);
                }
            }
            else if (address >= 0x094000 && address <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(1, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(1, offset, (byte)value);
                }
            }
            else if (address >= 0x098000 && address <= 0x09bfff)
            {
                int offset = (address - 0x098000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(2, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(2, offset, (byte)value);
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void M0WriteWord_A(int address, short value)
        {
            address &= 0xfffff;
            if (address >= 0x084000 && address + 1 <= 0x084001)
            {
                active_layers_w((ushort)value);
            }
            else if (address >= 0x084008 && address + 1 <= 0x084009)
            {
                megasys1_scrollx2_w((ushort)value);
            }
            else if (address >= 0x08400a && address + 1 <= 0x08400b)
            {
                megasys1_scrolly2_w((ushort)value);
            }
            else if (address >= 0x08400c && address + 1 <= 0x08400d)
            {
                megasys1_set_vreg_flag(2, (byte)value);
            }
            else if (address >= 0x084100 && address + 1 <= 0x084101)
            {
                sprite_flag_w((ushort)value);
            }
            else if (address >= 0x084200 && address + 1 <= 0x084201)
            {
                megasys1_scrollx0_w((ushort)value);
            }
            else if (address >= 0x084202 && address + 1 <= 0x084203)
            {
                megasys1_scrolly0_w((ushort)value);
            }
            else if (address >= 0x084204 && address + 1 <= 0x084205)
            {
                megasys1_set_vreg_flag(0, (ushort)value);
            }
            else if (address >= 0x084208 && address + 1 <= 0x084209)
            {
                megasys1_scrollx1_w((ushort)value);
            }
            else if (address >= 0x08420a && address + 1 <= 0x08420b)
            {
                megasys1_scrolly1_w((ushort)value);
            }
            else if (address >= 0x08420c && address + 1 <= 0x08420d)
            {
                megasys1_set_vreg_flag(1, (ushort)value);
            }
            else if (address >= 0x084300 && address + 1 <= 0x084301)
            {
                screen_flag_w((ushort)value);
            }
            else if (address >= 0x084308 && address + 1 <= 0x084309)
            {
                megasys1_soundlatch_w((ushort)value);
            }
            else if (address >= 0x088000 && address + 1 <= 0x0887ff)
            {
                int offset = (address - 0x088000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x08e000 && address + 1 <= 0x08ffff)
            {
                int offset = (address - 0x08e000) / 2;
                megasys1_objectram[offset] = (ushort)value;
            }
            else if (address >= 0x090000 && address + 1 <= 0x093fff)
            {
                int offset = (address - 0x090000) / 2;
                scrollram_w(0, offset, (ushort)value);
            }
            else if (address >= 0x094000 && address + 1 <= 0x097fff)
            {
                int offset = (address - 0x094000) / 2;
                scrollram_w(1, offset, (ushort)value);
            }
            else if (address >= 0x098000 && address + 1 <= 0x09bfff)
            {
                int offset = (address - 0x098000) / 2;
                scrollram_w(2, offset, (ushort)value);
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0f0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void M0WriteLong_A(int address, int value)
        {
            M0WriteWord_A(address, (short)(value >> 16));
            M0WriteWord_A(address + 2, (short)value);
        }
        public static sbyte M0ReadOpByte_B(int address)
        {
            address &= 0xfffff;
            sbyte result = 0;
            if (address <= 0x03ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x060000 && address <= 0x07ffff)
            {
                int offset = address - 0x060000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x080000 && address <= 0x0bffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            return result;
        }
        public static sbyte M0ReadByte_B(int address)
        {
            address &= 0xfffff;
            sbyte result = 0;
            if (address <= 0x03ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x044000 && address <= 0x044001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_active_layers >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_active_layers;
                }
            }
            else if (address >= 0x044008 && address <= 0x044009)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[2];
                }
            }
            else if (address >= 0x04400a && address <= 0x04400b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[2];
                }
            }
            else if (address >= 0x04400c && address <= 0x04400d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[2];
                }
            }
            else if (address >= 0x044100 && address <= 0x044101)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(sprite_flag_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)sprite_flag_r();
                }
            }
            else if (address >= 0x044200 && address <= 0x044201)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[0];
                }
            }
            else if (address >= 0x044202 && address <= 0x044203)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[0];
                }
            }
            else if (address >= 0x044204 && address <= 0x044205)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[0];
                }
            }
            else if (address >= 0x044208 && address <= 0x044209)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[1];
                }
            }
            else if (address >= 0x04420a && address <= 0x04420b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[1];
                }
            }
            else if (address >= 0x04420c && address <= 0x04420d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[1];
                }
            }
            else if (address >= 0x044300 && address <= 0x044301)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_screen_flag >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_screen_flag;
                }
            }
            else if (address >= 0x048000 && address <= 0x0487ff)
            {
                int offset = (address - 0x048000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x04e000 && address <= 0x04ffff)
            {
                int offset = (address - 0x04e000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_objectram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_objectram[offset];
                }
            }
            else if (address >= 0x050000 && address <= 0x053fff)
            {
                int offset = (address - 0x050000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[0][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[0][offset];
                }
            }
            else if (address >= 0x054000 && address <= 0x057fff)
            {
                int offset = (address - 0x054000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[1][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[1][offset];
                }
            }
            else if (address >= 0x058000 && address <= 0x05bfff)
            {
                int offset = (address - 0x058000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[2][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[2][offset];
                }
            }
            else if (address >= 0x060000 && address <= 0x07ffff)
            {
                int offset = address - 0x060000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x080000 && address <= 0x0bffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x0e0000 && address <= 0x0e0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(ip_select_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)ip_select_r();
                }
            }
            return result;
        }
        public static short M0ReadOpWord_B(int address)
        {
            address &= 0xfffff;
            short result = 0;
            if (address + 1 <= 0x03ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x060000 && address + 1 <= 0x07ffff)
            {
                int offset = address - 0x060000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x080000 && address + 1 <= 0x0bffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            return result;
        }
        public static short M0ReadWord_B(int address)
        {
            address &= 0xfffff;
            short result = 0;
            if (address + 1 <= 0x03ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x044000 && address + 1 <= 0x044001)
            {
                result = (short)megasys1_active_layers;
            }
            else if (address >= 0x044008 && address + 1 <= 0x044009)
            {
                result = (short)megasys1_scrollx[2];
            }
            else if (address >= 0x04400a && address + 1 <= 0x04400b)
            {
                result = (short)megasys1_scrolly[2];
            }
            else if (address >= 0x04400c && address + 1 <= 0x04400d)
            {
                result = (short)megasys1_scroll_flag[2];
            }
            else if (address >= 0x044100 && address + 1 <= 0x044101)
            {
                result = (short)sprite_flag_r();
            }
            else if (address >= 0x044200 && address + 1 <= 0x044201)
            {
                result = (short)megasys1_scrollx[0];
            }
            else if (address >= 0x044202 && address + 1 <= 0x044203)
            {
                result = (short)megasys1_scrolly[0];
            }
            else if (address >= 0x044204 && address + 1 <= 0x044205)
            {
                result = (short)megasys1_scroll_flag[0];
            }
            else if (address >= 0x044208 && address + 1 <= 0x044209)
            {
                result = (short)megasys1_scrollx[1];
            }
            else if (address >= 0x04420a && address + 1 <= 0x04420b)
            {
                result = (short)megasys1_scrolly[1];
            }
            else if (address >= 0x04420c && address + 1 <= 0x04420d)
            {
                result = (short)megasys1_scroll_flag[1];
            }
            else if (address >= 0x044300 && address + 1 <= 0x044301)
            {
                result = (short)megasys1_screen_flag;
            }
            else if (address >= 0x048000 && address + 1 <= 0x0487ff)
            {
                int offset = (address - 0x048000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x04e000 && address + 1 <= 0x04ffff)
            {
                int offset = (address - 0x04e000) / 2;
                result = (short)megasys1_objectram[offset];
            }
            else if (address >= 0x050000 && address + 1 <= 0x053fff)
            {
                int offset = (address - 0x050000) / 2;
                result = (short)megasys1_scrollram[0][offset];
            }
            else if (address >= 0x054000 && address + 1 <= 0x057fff)
            {
                int offset = (address - 0x054000) / 2;
                result = (short)megasys1_scrollram[1][offset];
            }
            else if (address >= 0x058000 && address + 1 <= 0x05bfff)
            {
                int offset = (address - 0x058000) / 2;
                result = (short)megasys1_scrollram[2][offset];
            }
            else if (address >= 0x060000 && address + 1 <= 0x07ffff)
            {
                int offset = address - 0x060000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x080000 && address + 1 <= 0x0bffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0e0001)
            {
                result = (short)ip_select_r();
            }
            return result;
        }
        public static int M0ReadOpLong_B(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadOpWord_B(address) * 0x10000 + (ushort)M0ReadOpWord_B(address + 2));
            return result;
        }
        public static int M0ReadLong_B(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadWord_B(address) * 0x10000 + (ushort)M0ReadWord_B(address + 2));
            return result;
        }
        public static void M0WriteByte_B(int address, sbyte value)
        {
            address &= 0xfffff;
            if (address >= 0x044000 && address <= 0x044001)
            {
                if (address % 2 == 0)
                {
                    active_layers_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    active_layers_w2((byte)value);
                }
            }
            else if (address >= 0x044008 && address <= 0x044009)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx2_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx2_w2((byte)value);
                }
            }
            else if (address >= 0x04400a && address <= 0x04400b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly2_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly2_w2((byte)value);
                }
            }
            else if (address >= 0x04400c && address <= 0x04400d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(2, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(2, (byte)value);
                }
            }
            else if (address >= 0x044100 && address <= 0x044101)
            {
                if (address % 2 == 0)
                {
                    sprite_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    sprite_flag_w2((byte)value);
                }
            }
            else if (address >= 0x044200 && address <= 0x044201)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx0_w2((byte)value);
                }
            }
            else if (address >= 0x044202 && address <= 0x044203)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly0_w2((byte)value);
                }
            }
            else if (address >= 0x044204 && address <= 0x044205)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(0, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(0, (byte)value);
                }
            }
            else if (address >= 0x044208 && address <= 0x044209)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx1_w2((byte)value);
                }
            }
            else if (address >= 0x04420a && address <= 0x04420b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly1_w2((byte)value);
                }
            }
            else if (address >= 0x04420c && address <= 0x04420d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(1, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(1, (byte)value);
                }
            }
            else if (address >= 0x044300 && address <= 0x044301)
            {
                if (address % 2 == 0)
                {
                    screen_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    screen_flag_w2((byte)value);
                }
            }
            else if (address >= 0x044308 && address <= 0x044309)
            {
                if (address % 2 == 0)
                {

                }
                else if (address % 2 == 1)
                {
                    megasys1_soundlatch_w2((byte)value);
                }
            }
            else if (address >= 0x048000 && address <= 0x0487ff)
            {
                int offset = (address - 0x048000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x04e000 && address <= 0x04ffff)
            {
                int offset = (address - 0x04e000) / 2;
                if (address % 2 == 0)
                {
                    megasys1_objectram[offset] = (ushort)((value << 8) | (megasys1_objectram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    megasys1_objectram[offset] = (ushort)((megasys1_objectram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x050000 && address <= 0x053fff)
            {
                int offset = (address - 0x050000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(0, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(0, offset, (byte)value);
                }
            }
            else if (address >= 0x054000 && address <= 0x057fff)
            {
                int offset = (address - 0x054000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(1, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(1, offset, (byte)value);
                }
            }
            else if (address >= 0x058000 && address <= 0x05bfff)
            {
                int offset = (address - 0x058000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(2, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(2, offset, (byte)value);
                }
            }
            else if (address >= 0x060000 && address <= 0x07ffff)
            {
                int offset = address - 0x060000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x0e0000 && address <= 0x0e0001)
            {
                if (address % 2 == 0)
                {
                    int i1 = 1;
                }
                else if (address % 2 == 1)
                {
                    ip_select_w((byte)value);
                }
            }
        }
        public static void M0WriteWord_B(int address, short value)
        {
            address &= 0xfffff;
            if (address >= 0x044000 && address + 1 <= 0x044001)
            {
                active_layers_w((ushort)value);
            }
            else if (address >= 0x044008 && address + 1 <= 0x044009)
            {
                megasys1_scrollx2_w((ushort)value);
            }
            else if (address >= 0x04400a && address + 1 <= 0x04400b)
            {
                megasys1_scrolly2_w((ushort)value);
            }
            else if (address >= 0x04400c && address + 1 <= 0x04400d)
            {
                megasys1_set_vreg_flag(2, (byte)value);
            }
            else if (address >= 0x044100 && address + 1 <= 0x044101)
            {
                sprite_flag_w((ushort)value);
            }
            else if (address >= 0x044200 && address + 1 <= 0x044201)
            {
                megasys1_scrollx0_w((ushort)value);
            }
            else if (address >= 0x044202 && address + 1 <= 0x044203)
            {
                megasys1_scrolly0_w((ushort)value);
            }
            else if (address >= 0x044204 && address + 1 <= 0x044205)
            {
                megasys1_set_vreg_flag(0, (ushort)value);
            }
            else if (address >= 0x044208 && address + 1 <= 0x044209)
            {
                megasys1_scrollx1_w((ushort)value);
            }
            else if (address >= 0x04420a && address + 1 <= 0x04420b)
            {
                megasys1_scrolly1_w((ushort)value);
            }
            else if (address >= 0x04420c && address + 1 <= 0x04420d)
            {
                megasys1_set_vreg_flag(1, (ushort)value);
            }
            else if (address >= 0x044300 && address + 1 <= 0x044301)
            {
                screen_flag_w((ushort)value);
            }
            else if (address >= 0x044308 && address + 1 <= 0x044309)
            {
                megasys1_soundlatch_w((ushort)value);
            }
            else if (address >= 0x048000 && address + 1 <= 0x0487ff)
            {
                int offset = (address - 0x048000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x04e000 && address + 1 <= 0x04ffff)
            {
                int offset = (address - 0x04e000) / 2;
                megasys1_objectram[offset] = (ushort)value;
            }
            else if (address >= 0x050000 && address + 1 <= 0x053fff)
            {
                int offset = (address - 0x050000) / 2;
                scrollram_w(0, offset, (ushort)value);
            }
            else if (address >= 0x054000 && address + 1 <= 0x057fff)
            {
                int offset = (address - 0x054000) / 2;
                scrollram_w(1, offset, (ushort)value);
            }
            else if (address >= 0x058000 && address + 1 <= 0x05bfff)
            {
                int offset = (address - 0x058000) / 2;
                scrollram_w(2, offset, (ushort)value);
            }
            else if (address >= 0x060000 && address + 1 <= 0x07ffff)
            {
                int offset = address - 0x060000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0e0001)
            {
                ip_select_w((ushort)value);
            }
        }
        public static void M0WriteLong_B(int address, int value)
        {
            M0WriteWord_B(address, (short)(value >> 16));
            M0WriteWord_B(address + 2, (short)value);
        }
        public static sbyte M0ReadOpByte_C(int address)
        {
            address &= 0x1fffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x1f0000 && address <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte M0ReadByte_C(int address)
        {
            address &= 0x1fffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)Memory.mainrom[address];
            }
            else if (address >= 0x0c2000 && address <= 0x0c2001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[0];
                }
            }
            else if (address >= 0x0c2002 && address <= 0x0c2003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[0];
                }
            }
            else if (address >= 0x0c2004 && address <= 0x0c2005)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[0];
                }
            }
            else if (address >= 0x0c2008 && address <= 0x0c2009)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[1];
                }
            }
            else if (address >= 0x0c200a && address <= 0x0c200b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[1];
                }
            }
            else if (address >= 0x0c200c && address <= 0x0c200d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[1];
                }
            }
            else if (address >= 0x0c2100 && address <= 0x0c2101)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[2];
                }
            }
            else if (address >= 0x0c2102 && address <= 0x0c2103)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[2];
                }
            }
            else if (address >= 0x0c2104 && address <= 0x0c2105)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[2] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[2];
                }
            }
            else if (address >= 0x0c2108 && address <= 0x0c2109)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_sprite_bank >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_sprite_bank;
                }
            }
            else if (address >= 0x0c2200 && address <= 0x0c2201)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(sprite_flag_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)sprite_flag_r();
                }
            }
            else if (address >= 0x0c2208 && address <= 0x0c2209)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_active_layers >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_active_layers;
                }
            }
            else if (address >= 0x0c2308 && address <= 0x0c2309)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_screen_flag >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_screen_flag;
                }
            }
            else if (address >= 0x0c8000 && address <= 0x0c8001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Sound.soundlatch_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Sound.soundlatch_r();
                }
            }
            else if (address >= 0x0d2000 && address <= 0x0d3fff)
            {
                int offset = (address - 0x0d2000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_objectram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_objectram[offset];
                }
            }
            else if (address >= 0x0d8000 && address <= 0x0d8001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(ip_select_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)ip_select_r();
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0e3fff)
            {
                int offset = (address - 0x0e0000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[0][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[0][offset];
                }
            }
            else if (address >= 0x0e8000 && address <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[1][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[1][offset];
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0f3fff)
            {
                int offset = (address - 0x0f0000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[2][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[2][offset];
                }
            }
            else if (address >= 0x0f8000 && address <= 0x0f87ff)
            {
                int offset = (address - 0x0f8000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x1f0000 && address <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short M0ReadOpWord_C(int address)
        {
            address &= 0x1fffff;
            short result = 0;
            if (address + 1 <= 0x07ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x1f0000 && address + 1 <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short M0ReadWord_C(int address)
        {
            address &= 0x1fffff;
            short result = 0;
            if (address + 1 <= 0x07ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x0c2000 && address + 1 <= 0x0c2001)
            {
                result = (short)megasys1_scrollx[0];
            }
            else if (address >= 0x0c2002 && address + 1 <= 0x0c2003)
            {
                result = (short)megasys1_scrolly[0];
            }
            else if (address >= 0x0c2004 && address + 1 <= 0x0c2005)
            {
                result = (short)megasys1_scroll_flag[0];
            }
            else if (address >= 0x0c2008 && address + 1 <= 0x0c2009)
            {
                result = (short)megasys1_scrollx[1];
            }
            else if (address >= 0x0c200a && address + 1 <= 0x0c200b)
            {
                result = (short)megasys1_scrolly[1];
            }
            else if (address >= 0x0c200c && address + 1 <= 0x0c200d)
            {
                result = (short)megasys1_scroll_flag[1];
            }
            else if (address >= 0x0c2100 && address + 1 <= 0x0c2101)
            {
                result = (short)megasys1_scrollx[2];
            }
            else if (address >= 0x0c2102 && address + 1 <= 0x0c2103)
            {
                result = (short)megasys1_scrolly[2];
            }
            else if (address >= 0x0c2104 && address + 1 <= 0x0c2105)
            {
                result = (short)megasys1_scroll_flag[2];
            }
            else if (address >= 0x0c2108 && address + 1 <= 0x0c2109)
            {
                result = (short)megasys1_sprite_bank;
            }
            else if (address >= 0x0c2200 && address + 1 <= 0x0c2201)
            {
                result = (short)sprite_flag_r();
            }
            else if (address >= 0x0c2208 && address + 1 <= 0x0c2209)
            {
                result = (short)megasys1_active_layers;
            }
            else if (address >= 0x0c2308 && address + 1 <= 0x0c2309)
            {
                result = (short)megasys1_screen_flag;
            }
            else if (address >= 0x0c8000 && address + 1 <= 0x0c8001)
            {
                result = (short)Sound.soundlatch_r();
            }
            else if (address >= 0x0d2000 && address + 1 <= 0x0d3fff)
            {
                int offset = (address - 0x0d2000) / 2;
                result = (short)megasys1_objectram[offset];
            }
            else if (address >= 0x0d8000 && address + 1 <= 0x0d8001)
            {
                result = (short)ip_select_r();
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0e3fff)
            {
                int offset = (address - 0x0e0000) / 2;
                result = (short)megasys1_scrollram[0][offset];
            }
            else if (address >= 0x0e8000 && address + 1 <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                result = (short)megasys1_scrollram[1][offset];
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0f3fff)
            {
                int offset = (address - 0x0f0000) / 2;
                result = (short)megasys1_scrollram[2][offset];
            }
            else if (address >= 0x0f8000 && address + 1 <= 0x0f87ff)
            {
                int offset = (address - 0x0f8000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x1f0000 && address + 1 <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int M0ReadOpLong_C(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadOpWord_C(address) * 0x10000 + (ushort)M0ReadOpWord_C(address + 2));
            return result;
        }
        public static int M0ReadLong_C(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadWord_C(address) * 0x10000 + (ushort)M0ReadWord_C(address + 2));
            return result;
        }
        public static void M0WriteByte_C(int address, sbyte value)
        {
            address &= 0x1fffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                Memory.mainrom[address] = (byte)value;
            }
            else if (address >= 0x0c2000 && address <= 0x0c2001)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx0_w2((byte)value);
                }
            }
            else if (address >= 0x0c2002 && address <= 0x0c2003)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly0_w2((byte)value);
                }
            }
            else if (address >= 0x0c2004 && address <= 0x0c2005)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(0, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(0, (byte)value);
                }
            }
            else if (address >= 0x0c2008 && address <= 0x0c2009)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx1_w2((byte)value);
                }
            }
            else if (address >= 0x0c200a && address <= 0x0c200b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly1_w2((byte)value);
                }
            }
            else if (address >= 0x0c200c && address <= 0x0c200d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(1, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(1, (byte)value);
                }
            }
            else if (address >= 0x0c2100 && address <= 0x0c2101)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx2_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx2_w2((byte)value);
                }
            }
            else if (address >= 0x0c2102 && address <= 0x0c2103)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly2_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly2_w2((byte)value);
                }
            }
            else if (address >= 0x0c2104 && address <= 0x0c2105)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(2, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(2, (byte)value);
                }
            }
            else if (address >= 0x0c2108 && address <= 0x0c2109)
            {
                if (address % 2 == 0)
                {
                    sprite_bank_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    sprite_bank_w2((byte)value);
                }
            }
            else if (address >= 0x0c2200 && address <= 0x0c2201)
            {
                if (address % 2 == 0)
                {
                    sprite_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    sprite_flag_w2((byte)value);
                }
            }
            else if (address >= 0x0c2208 && address <= 0x0c2209)
            {
                if (address % 2 == 0)
                {
                    active_layers_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    active_layers_w2((byte)value);
                }
            }
            else if (address >= 0x0c2308 && address <= 0x0c2309)
            {
                if (address % 2 == 0)
                {
                    screen_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    screen_flag_w2((byte)value);
                }
            }
            else if (address >= 0x0c8000 && address <= 0x0c8001)
            {
                if (address % 2 == 0)
                {

                }
                else if (address % 2 == 1)
                {
                    soundlatch_c_w2((byte)value);
                }
            }
            else if (address >= 0x0d2000 && address <= 0x0d3fff)
            {
                int offset = (address - 0x0d2000) / 2;
                if (address % 2 == 0)
                {
                    megasys1_objectram[offset] = (ushort)((value << 8) | (megasys1_objectram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    megasys1_objectram[offset] = (ushort)((megasys1_objectram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x0d8000 && address <= 0x0d8001)
            {
                if (address % 2 == 1)
                {
                    ip_select_w((byte)value);
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0e3fff)
            {
                int offset = (address - 0x0e0000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(0, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(0, offset, (byte)value);
                }
            }
            else if (address >= 0x0e8000 && address <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(1, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(1, offset, (byte)value);
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0f3fff)
            {
                int offset = (address - 0x0f0000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(2, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(2, offset, (byte)value);
                }
            }
            else if (address >= 0x0f8000 && address <= 0x0f87ff)
            {
                int offset = (address - 0x0f8000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x1f0000 && address <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void M0WriteWord_C(int address, short value)
        {
            address &= 0x1fffff;
            if (address >= 0x000000 && address + 1 <= 0x07ffff)
            {
                Memory.mainrom[address] = (byte)(value >> 8);
                Memory.mainrom[address + 1] = (byte)value;
            }
            else if (address >= 0x0c2000 && address + 1 <= 0x0c2001)
            {
                megasys1_scrollx0_w((ushort)value);
            }
            else if (address >= 0x0c2002 && address + 1 <= 0x0c2003)
            {
                megasys1_scrolly0_w((ushort)value);
            }
            else if (address >= 0x0c2004 && address + 1 <= 0x0c2005)
            {
                megasys1_set_vreg_flag(0, (ushort)value);
            }
            else if (address >= 0x0c2008 && address + 1 <= 0x0c2009)
            {
                megasys1_scrollx1_w((ushort)value);
            }
            else if (address >= 0x0c200a && address + 1 <= 0x0c200b)
            {
                megasys1_scrolly1_w((ushort)value);
            }
            else if (address >= 0x0c200c && address + 1 <= 0x0c200d)
            {
                megasys1_set_vreg_flag(1, (ushort)value);
            }
            else if (address >= 0x0c2100 && address + 1 <= 0x0c2101)
            {
                megasys1_scrollx2_w((ushort)value);
            }
            else if (address >= 0x0c2102 && address + 1 <= 0x0c2103)
            {
                megasys1_scrolly2_w((ushort)value);
            }
            else if (address >= 0x0c2104 && address + 1 <= 0x0c2105)
            {
                megasys1_set_vreg_flag(2, (byte)value);
            }
            else if (address >= 0x0c2108 && address + 1 <= 0x0c2109)
            {
                sprite_bank_w((ushort)value);
            }
            else if (address >= 0x0c2200 && address + 1 <= 0x0c2201)
            {
                sprite_flag_w((ushort)value);
            }
            else if (address >= 0x0c2208 && address + 1 <= 0x0c2209)
            {
                active_layers_w((ushort)value);
            }
            else if (address >= 0x0c2308 && address + 1 <= 0x0c2309)
            {
                screen_flag_w((ushort)value);
            }
            else if (address >= 0x0c8000 && address + 1 <= 0x0c8001)
            {
                soundlatch_c_w((ushort)value);
            }
            else if (address >= 0x0d2000 && address + 1 <= 0x0d3fff)
            {
                int offset = (address - 0x0d2000) / 2;
                megasys1_objectram[offset] = (ushort)value;
            }
            else if (address >= 0x0d8000 && address + 1 <= 0x0d8001)
            {
                ip_select_w((ushort)value);
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0e3fff)
            {
                int offset = (address - 0x0e0000) / 2;
                scrollram_w(0, offset, (ushort)value);
            }
            else if (address >= 0x0e8000 && address + 1 <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                scrollram_w(1, offset, (ushort)value);
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0f3fff)
            {
                int offset = (address - 0x0f0000) / 2;
                scrollram_w(2, offset, (ushort)value);
            }
            else if (address >= 0x0f8000 && address + 1 <= 0x0f87ff)
            {
                int offset = (address - 0x0f8000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x1f0000 && address + 1 <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void M0WriteLong_C(int address, int value)
        {
            M0WriteWord_C(address, (short)(value >> 16));
            M0WriteWord_C(address + 2, (short)value);
        }
        public static sbyte M0ReadOpByte_D(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x03ffff)
            {
                result = (sbyte)Memory.mainrom[address];
            }
            else if (address >= 0x1f0000 && address <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte M0ReadByte_D(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x03ffff)
            {
                result = (sbyte)Memory.mainrom[address];
            }
            else if (address >= 0x0c2000 && address <= 0x0c2001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[0];
                }
            }
            else if (address >= 0x0c2002 && address <= 0x0c2003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[0];
                }
            }
            else if (address >= 0x0c2004 && address <= 0x0c2005)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[0] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[0];
                }
            }
            else if (address >= 0x0c2008 && address <= 0x0c2009)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollx[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollx[1];
                }
            }
            else if (address >= 0x0c200a && address <= 0x0c200b)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrolly[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrolly[1];
                }
            }
            else if (address >= 0x0c200c && address <= 0x0c200d)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scroll_flag[1] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scroll_flag[1];
                }
            }
            else if (address >= 0x0c2200 && address <= 0x0c2201)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(sprite_flag_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)sprite_flag_r();
                }
            }
            else if (address >= 0x0c2208 && address <= 0x0c2209)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_active_layers >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_active_layers;
                }
            }
            else if (address >= 0x0c2308 && address <= 0x0c2309)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_screen_flag >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_screen_flag;
                }
            }
            else if (address >= 0x0ca000 && address <= 0x0cbfff)
            {
                int offset = (address - 0x0ca000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_objectram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_objectram[offset];
                }
            }
            else if (address >= 0x0d0000 && address <= 0x0d3fff)
            {
                int offset = (address - 0x0d0000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[1][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[1][offset];
                }
            }
            else if (address >= 0x0d8000 && address <= 0x0d87ff)
            {
                int offset = (address - 0x0d8000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0e0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)dsw1;
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)dsw2;
                }
            }
            else if (address >= 0x0e8000 && address <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(megasys1_scrollram[0][offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)megasys1_scrollram[0][offset];
                }
            }
            else if (address >= 0x0f0000 && address <= 0x0f0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(shorts >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)shorts;
                }
            }
            else if (address >= 0x0f8000 && address <= 0x0f8001)
            {
                if (address % 2 == 1)
                {
                    result = (sbyte)OKI6295.oo1[0].okim6295_status_r();
                }
            }
            else if (address >= 0x1f0000 && address <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short M0ReadOpWord_D(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x03ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x1f0000 && address + 1 <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short M0ReadWord_D(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x03ffff)
            {
                result = (short)(Memory.mainrom[address] * 0x100 + Memory.mainrom[address + 1]);
            }
            else if (address >= 0x0c2000 && address + 1 <= 0x0c2001)
            {
                result = (short)megasys1_scrollx[0];
            }
            else if (address >= 0x0c2002 && address + 1 <= 0x0c2003)
            {
                result = (short)megasys1_scrolly[0];
            }
            else if (address >= 0x0c2004 && address + 1 <= 0x0c2005)
            {
                result = (short)megasys1_scroll_flag[0];
            }
            else if (address >= 0x0c2008 && address + 1 <= 0x0c2009)
            {
                result = (short)megasys1_scrollx[1];
            }
            else if (address >= 0x0c200a && address + 1 <= 0x0c200b)
            {
                result = (short)megasys1_scrolly[1];
            }
            else if (address >= 0x0c200c && address + 1 <= 0x0c200d)
            {
                result = (short)megasys1_scroll_flag[1];
            }
            else if (address >= 0x0c2200 && address + 1 <= 0x0c2201)
            {
                result = (short)sprite_flag_r();
            }
            else if (address >= 0x0c2208 && address + 1 <= 0x0c2209)
            {
                result = (short)megasys1_active_layers;
            }
            else if (address >= 0x0c2308 && address + 1 <= 0x0c2309)
            {
                result = (short)megasys1_screen_flag;
            }
            else if (address >= 0x0ca000 && address + 1 <= 0x0cbfff)
            {
                int offset = (address - 0x0ca000) / 2;
                result = (short)megasys1_objectram[offset];
            }
            else if (address >= 0x0d0000 && address + 1 <= 0x0d3fff)
            {
                int offset = (address - 0x0d0000) / 2;
                result = (short)megasys1_scrollram[1][offset];
            }
            else if (address >= 0x0d8000 && address + 1 <= 0x0d87ff)
            {
                int offset = (address - 0x0d8000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0e0001)
            {
                result = (short)dsw_r();
            }
            else if (address >= 0x0e8000 && address + 1 <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                result = (short)megasys1_scrollram[0][offset];
            }
            else if (address >= 0x0f0000 && address + 1 <= 0x0f0001)
            {
                result = shorts;
            }
            else if (address >= 0x0f8000 && address + 1 <= 0x0f8001)
            {
                result = (short)OKI6295.oo1[0].okim6295_status_r();
            }
            else if (address >= 0x1f0000 && address + 1 <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int M0ReadOpLong_D(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadOpWord_D(address) * 0x10000 + (ushort)M0ReadOpWord_D(address + 2));
            return result;
        }
        public static int M0ReadLong_D(int address)
        {
            int result = 0;
            result = (int)((ushort)M0ReadWord_D(address) * 0x10000 + (ushort)M0ReadWord_D(address + 2));
            return result;
        }
        public static void M0WriteByte_D(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address <= 0x03ffff)
            {
                Memory.mainrom[address] = (byte)value;
            }
            else if (address >= 0x0c2000 && address <= 0x0c2001)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx0_w2((byte)value);
                }
            }
            else if (address >= 0x0c2002 && address <= 0x0c2003)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly0_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly0_w2((byte)value);
                }
            }
            else if (address >= 0x0c2004 && address <= 0x0c2005)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(0, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(0, (byte)value);
                }
            }
            else if (address >= 0x0c2008 && address <= 0x0c2009)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrollx1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrollx1_w2((byte)value);
                }
            }
            else if (address >= 0x0c200a && address <= 0x0c200b)
            {
                if (address % 2 == 0)
                {
                    megasys1_scrolly1_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_scrolly1_w2((byte)value);
                }
            }
            else if (address >= 0x0c200c && address <= 0x0c200d)
            {
                if (address % 2 == 0)
                {
                    megasys1_set_vreg_flag1(1, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    megasys1_set_vreg_flag2(1, (byte)value);
                }
            }
            else if (address >= 0x0c2108 && address <= 0x0c2109)
            {

            }
            else if (address >= 0x0c2200 && address <= 0x0c2201)
            {
                if (address % 2 == 0)
                {
                    sprite_flag_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    sprite_flag_w2((byte)value);
                }
            }
            else if (address >= 0x0c2208 && address <= 0x0c2209)
            {
                if (address % 2 == 0)
                {
                    active_layers_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    active_layers_w2((byte)value);
                }
            }
            else if (address >= 0x0c2308 && address <= 0x0c2309)
            {
                if (address % 2 == 0)
                {
                    screen_flag_wd1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    screen_flag_wd2((byte)value);
                }
            }
            else if (address >= 0x0ca000 && address <= 0x0cbfff)
            {
                int offset = (address - 0x0ca000) / 2;
                if (address % 2 == 0)
                {
                    megasys1_objectram[offset] = (ushort)((value << 8) | (megasys1_objectram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    megasys1_objectram[offset] = (ushort)((megasys1_objectram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x0d0000 && address <= 0x0d3fff)
            {
                int offset = (address - 0x0d0000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(1, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(1, offset, (byte)value);
                }
            }
            else if (address >= 0x0d8000 && address <= 0x0d87ff)
            {
                int offset = (address - 0x0d8000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRRGGGGGBBBBBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRRGGGGGBBBBBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x0e8000 && address <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                if (address % 2 == 0)
                {
                    scrollram_w1(0, offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    scrollram_w2(0, offset, (byte)value);
                }
            }
            else if (address >= 0x0f8000 && address <= 0x0f8001)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[0].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0x1f0000 && address <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void M0WriteWord_D(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x03ffff)
            {
                Memory.mainrom[address] = (byte)(value >> 8);
                Memory.mainrom[address + 1] = (byte)value;
            }
            else if (address >= 0x0c2000 && address + 1 <= 0x0c2001)
            {
                megasys1_scrollx0_w((ushort)value);
            }
            else if (address >= 0x0c2002 && address + 1 <= 0x0c2003)
            {
                megasys1_scrolly0_w((ushort)value);
            }
            else if (address >= 0x0c2004 && address + 1 <= 0x0c2005)
            {
                megasys1_set_vreg_flag(0, (ushort)value);
            }
            else if (address >= 0x0c2008 && address + 1 <= 0x0c2009)
            {
                megasys1_scrollx1_w((ushort)value);
            }
            else if (address >= 0x0c200a && address + 1 <= 0x0c200b)
            {
                megasys1_scrolly1_w((ushort)value);
            }
            else if (address >= 0x0c200c && address + 1 <= 0x0c200d)
            {
                megasys1_set_vreg_flag(1, (ushort)value);
            }
            else if (address >= 0x0c2108 && address + 1 <= 0x0c2109)
            {

            }
            else if (address >= 0x0c2200 && address + 1 <= 0x0c2201)
            {
                sprite_flag_w((ushort)value);
            }
            else if (address >= 0x0c2208 && address + 1 <= 0x0c2209)
            {
                active_layers_w((ushort)value);
            }
            else if (address >= 0x0c2308 && address + 1 <= 0x0c2309)
            {
                screen_flag_wd((ushort)value);
            }
            else if (address >= 0x0ca000 && address + 1 <= 0x0cbfff)
            {
                int offset = (address - 0x0ca000) / 2;
                megasys1_objectram[offset] = (ushort)value;
            }
            else if (address >= 0x0d0000 && address + 1 <= 0x0d3fff)
            {
                int offset = (address - 0x0d0000) / 2;
                scrollram_w(1, offset, (ushort)value);
            }
            else if (address >= 0x0d8000 && address + 1 <= 0x0d87ff)
            {
                int offset = (address - 0x0d8000) / 2;
                Generic.paletteram16_RRRRRGGGGGBBBBBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x0e8000 && address + 1 <= 0x0ebfff)
            {
                int offset = (address - 0x0e8000) / 2;
                scrollram_w(0, offset, (ushort)value);
            }
            else if (address >= 0x0f8000 && address + 1 <= 0x0f8001)
            {
                OKI6295.oo1[0].okim6295_data_w((ushort)value);
            }
            else if (address >= 0x1f0000 && address + 1 <= 0x1fffff)
            {
                int offset = address - 0x1f0000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void M0WriteLong_D(int address, int value)
        {
            M0WriteWord_D(address, (short)(value >> 16));
            M0WriteWord_D(address + 2, (short)value);
        }
        public static sbyte M1ReadOpByte_A(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address >= 0 && address <= 0x01ffff)
            {
                result = (sbyte)(Memory.audiorom[address]);
            }
            else if (address >= 0x0e0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0e0000;
                result = (sbyte)Memory.audioram[offset];
            }
            return result;
        }
        public static short M1ReadOpWord_A(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x01ffff)
            {
                result = (short)(Memory.audiorom[address] * 0x100 + Memory.audiorom[address + 1]);
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0e0000;
                result = (short)(Memory.audioram[offset] * 0x100 + Memory.audioram[offset + 1]);
            }
            return result;
        }
        public static int M1ReadOpLong_A(int address)
        {
            int result = 0;
            result = (int)((ushort)M1ReadOpWord_A(address) * 0x10000 + (ushort)M1ReadOpWord_A(address + 2));
            return result;
        }
        public static sbyte M1ReadByte_A(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address >= 0 && address <= 0x01ffff)
            {
                result = (sbyte)Memory.audiorom[address];
            }
            else if (address >= 0x040000 && address <= 0x040001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Sound.soundlatch_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Sound.soundlatch_r();
                }
            }
            else if (address >= 0x080002 && address <= 0x080003)
            {
                if (address % 2 == 1)
                {
                    result = (sbyte)YM2151.ym2151_status_port_0_r();
                }
            }
            else if (address >= 0x0a0000 && address <= 0x0a0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(oki_status_0_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)oki_status_0_r();
                }
            }
            else if (address >= 0x0c0000 && address <= 0x0c0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(oki_status_1_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)oki_status_1_r();
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0e0000;
                result = (sbyte)Memory.audioram[offset];
            }
            return result;
        }
        public static short M1ReadWord_A(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x01ffff)
            {
                result = (short)(Memory.audiorom[address] * 0x100 + Memory.audiorom[address + 1]);
            }
            else if (address >= 0x040000 && address + 1 <= 0x040001)
            {
                result = (short)Sound.soundlatch_r();
            }
            else if (address >= 0x080002 && address + 1 <= 0x080003)
            {
                result = (short)YM2151.ym2151_status_port_0_r();
            }
            else if (address >= 0x0a0000 && address + 1 <= 0x0a0001)
            {
                result = (short)oki_status_0_r();
            }
            else if (address >= 0x0c0000 && address + 1 <= 0x0c0001)
            {
                result = (short)oki_status_1_r();
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0e0000;
                result = (short)(Memory.audioram[offset] * 0x100 + Memory.audioram[offset + 1]);
            }
            return result;
        }
        public static int M1ReadLong_A(int address)
        {
            int result = 0;
            result = (int)((ushort)M1ReadWord_A(address) * 0x10000 + (ushort)M1ReadWord_A(address + 2));
            return result;
        }
        public static void M1WriteByte_A(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x01ffff)
            {
                Memory.audiorom[address] = (byte)value;
            }
            else if (address >= 0x060000 && address <= 0x060001)
            {
                Sound.soundlatch2_w((byte)value);
            }
            else if (address >= 0x080000 && address <= 0x080001)
            {
                if (address % 2 == 1)
                {
                    YM2151.ym2151_register_port_0_w((byte)value);
                }
            }
            else if (address >= 0x080002 && address <= 0x080003)
            {
                if (address % 2 == 1)
                {
                    YM2151.ym2151_data_port_0_w((byte)value);
                }
            }
            else if (address >= 0x0a0000 && address <= 0x0a0003)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[0].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0x0c0000 && address <= 0x0c0003)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[1].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0fffff)
            {
                int offset = address - 0x0e0000;
                Memory.audioram[offset] = (byte)value;
            }
        }
        public static void M1WriteWord_A(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x01ffff)
            {
                Memory.audiorom[address] = (byte)(value >> 8);
                Memory.audiorom[address + 1] = (byte)value;
            }
            else if (address >= 0x060000 && address + 1 <= 0x060001)
            {
                Sound.soundlatch2_w((ushort)value);
            }
            else if (address >= 0x080000 && address + 1 <= 0x080001)
            {
                YM2151.ym2151_register_port_0_w((byte)value);
            }
            else if (address >= 0x080002 && address + 1 <= 0x080003)
            {
                YM2151.ym2151_data_port_0_w((byte)value);
            }
            else if (address >= 0x0a0000 && address + 1 <= 0x0a0003)
            {
                OKI6295.oo1[0].okim6295_data_w((ushort)value);
            }
            else if (address >= 0x0c0000 && address + 1 <= 0x0c0003)
            {
                OKI6295.oo1[1].okim6295_data_w((ushort)value);
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0fffff)
            {
                int offset = address - 0x0e0000;
                Memory.audioram[offset] = (byte)(value >> 8);
                Memory.audioram[offset + 1] = (byte)value;
            }
        }
        public static void M1WriteLong_A(int address, int value)
        {
            M1WriteWord_A(address, (short)(value >> 16));
            M1WriteWord_A(address + 2, (short)value);
        }
        public static sbyte M1ReadOpByte_B(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address >= 0 && address <= 0x01ffff)
            {
                result = (sbyte)(Memory.audiorom[address]);
            }
            else if (address >= 0x0e0000 && address <= 0x0effff)
            {
                int offset = address - 0x0e0000;
                result = (sbyte)Memory.audioram[offset];
            }
            return result;
        }
        public static sbyte M1ReadByte_B(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address >= 0 && address <= 0x01ffff)
            {
                result = (sbyte)(Memory.audiorom[address]);
            }
            else if (address >= 0x040000 && address <= 0x040001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Sound.soundlatch_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Sound.soundlatch_r();
                }
            }
            else if (address >= 0x060000 && address <= 0x060001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Sound.soundlatch_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Sound.soundlatch_r();
                }
            }
            else if (address >= 0x080002 && address <= 0x080003)
            {
                if (address % 2 == 1)
                {
                    result = (sbyte)YM2151.ym2151_status_port_0_r();
                }
            }
            else if (address >= 0x0a0000 && address <= 0x0a0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(oki_status_0_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)oki_status_0_r();
                }
            }
            else if (address >= 0x0c0000 && address <= 0x0c0001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(oki_status_1_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)oki_status_1_r();
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0effff)
            {
                int offset = address - 0x0e0000;
                result = (sbyte)Memory.audioram[offset];
            }
            return result;
        }
        public static short M1ReadOpWord_B(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x01ffff)
            {
                result = (short)(Memory.audiorom[address] * 0x100 + Memory.audiorom[address + 1]);
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0effff)
            {
                int offset = address - 0x0e0000;
                result = (short)(Memory.audioram[offset] * 0x100 + Memory.audioram[offset + 1]);
            }
            return result;
        }
        public static int M1ReadOpLong_B(int address)
        {
            int result = 0;
            result = (int)((ushort)M1ReadOpWord_B(address) * 0x10000 + (ushort)M1ReadOpWord_B(address + 2));
            return result;
        }
        public static short M1ReadWord_B(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address >= 0 && address + 1 <= 0x01ffff)
            {
                result = (short)(Memory.audiorom[address] * 0x100 + Memory.audiorom[address + 1]);
            }
            else if (address >= 0x040000 && address + 1 <= 0x040001)
            {
                result = (short)Sound.soundlatch_r();
            }
            else if (address >= 0x060000 && address + 1 <= 0x060001)
            {
                result = (short)Sound.soundlatch_r();
            }
            else if (address >= 0x080002 && address + 1 <= 0x080003)
            {
                result = (short)YM2151.ym2151_status_port_0_r();
            }
            else if (address >= 0x0a0000 && address + 1 <= 0x0a0001)
            {
                result = (short)oki_status_0_r();
            }
            else if (address >= 0x0c0000 && address + 1 <= 0x0c0001)
            {
                result = (short)oki_status_1_r();
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0effff)
            {
                int offset = address - 0x0e0000;
                result = (short)(Memory.audioram[offset] * 0x100 + Memory.audioram[offset + 1]);
            }
            return result;
        }
        public static int M1ReadLong_B(int address)
        {
            int result = 0;
            result = (int)((ushort)M1ReadWord_B(address) * 0x10000 + (ushort)M1ReadWord_B(address + 2));
            return result;
        }
        public static void M1WriteByte_B(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x01ffff)
            {
                Memory.audiorom[address] = (byte)value;
            }
            else if (address >= 0x040000 && address <= 0x040001)
            {
                Sound.soundlatch2_w((byte)value);
            }
            else if (address >= 0x060000 && address <= 0x060001)
            {
                Sound.soundlatch2_w((byte)value);
            }
            else if (address >= 0x080000 && address <= 0x080001)
            {
                if (address % 2 == 1)
                {
                    YM2151.ym2151_register_port_0_w((byte)value);
                }
            }
            else if (address >= 0x080002 && address <= 0x080003)
            {
                if (address % 2 == 1)
                {
                    YM2151.ym2151_data_port_0_w((byte)value);
                }
            }
            else if (address >= 0x0a0000 && address <= 0x0a0003)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[0].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0x0c0000 && address <= 0x0c0003)
            {
                if (address % 2 == 1)
                {
                    OKI6295.oo1[1].okim6295_data_w((byte)value);
                }
            }
            else if (address >= 0x0e0000 && address <= 0x0effff)
            {
                int offset = address - 0x0e0000;
                Memory.audioram[offset] = (byte)value;
            }
        }
        public static void M1WriteWord_B(int address, short value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 1 <= 0x01ffff)
            {
                Memory.audiorom[address] = (byte)(value >> 8);
                Memory.audiorom[address + 1] = (byte)value;
            }
            else if (address >= 0x040000 && address + 1 <= 0x040001)
            {
                Sound.soundlatch2_w((ushort)value);
            }
            else if (address >= 0x060000 && address + 1 <= 0x060001)
            {
                Sound.soundlatch2_w((ushort)value);
            }
            else if (address >= 0x080000 && address + 1 <= 0x080001)
            {
                YM2151.ym2151_register_port_0_w((byte)value);
            }
            else if (address >= 0x080002 && address + 1 <= 0x080003)
            {
                YM2151.ym2151_data_port_0_w((byte)value);
            }
            else if (address >= 0x0a0000 && address + 1 <= 0x0a0003)
            {
                OKI6295.oo1[0].okim6295_data_w((ushort)value);
            }
            else if (address >= 0x0c0000 && address + 1 <= 0x0c0003)
            {
                OKI6295.oo1[1].okim6295_data_w((ushort)value);
            }
            else if (address >= 0x0e0000 && address + 1 <= 0x0effff)
            {
                int offset = address - 0x0e0000;
                Memory.audioram[offset] = (byte)(value >> 8);
                Memory.audioram[offset + 1] = (byte)value;
            }
        }
        public static void M1WriteLong_B(int address, int value)
        {
            M1WriteWord_B(address, (short)(value >> 16));
            M1WriteWord_B(address + 2, (short)value);
        }
        public static byte ZReadOp(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address];
            }
            return result;
        }
        public static byte ZReadMemory(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address];
            }
            else if (address >= 0xc000 && address <= 0xc7ff)
            {
                int offset = address - 0xc000;
                result = Memory.audioram[offset];
            }
            else if (address == 0xe000)
            {
                result = (byte)Sound.soundlatch_r();
            }
            return result;
        }
        public static void ZWriteMemory(ushort address, byte value)
        {
            if (address <= 0x3fff)
            {
                Memory.audiorom[address] = value;
            }
            else if (address >= 0xc000 && address <= 0xc7ff)
            {
                int offset = address - 0xc000;
                Memory.audioram[offset] = value;
            }
            else if (address == 0xf000)
            {

            }
        }
        public static byte ZReadHardware(ushort address)
        {
            address &= 0xff;
            byte result = 0;
            if (address == 0x00)
            {
                result = YM2203.ym2203_status_port_0_r();
            }
            return result;
        }
        public static void ZWriteHardware(ushort address, byte value)
        {
            address &= 0xff;
            if (address == 0x00)
            {
                YM2203.ym2203_control_port_0_w(value);
            }
            else if (address == 0x01)
            {
                YM2203.ym2203_write_port_0_w(value);
            }
        }
    }
}
