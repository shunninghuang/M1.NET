using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.z80;

namespace mame
{
    public partial class Taitob
    {
        public static sbyte MReadOpByte_masterw(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x203fff)
            {
                int offset = address - 0x200000;
                result=(sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_masterw(int address)
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
            else if (address >= 0x200000 && address <= 0x203fff)
            {
                int offset = address - 0x200000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x800000 && address <= 0x800001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_portreg_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_portreg_r();
                }
            }
            else if (address >= 0x800002 && address <= 0x800003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_port_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_port_r();
                }
            }
            else if (address >= 0xa00000 && address <= 0xa00001)
            {
                result = 0;
            }
            else if (address >= 0xa00002 && address <= 0xa00003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            return result;
        }
        public static short MReadOpWord_masterw(int address)
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
            else if (address >= 0x200000 && address <= 0x203fff)
            {
                int offset = address - 0x200000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_masterw(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x203fff)
            {
                int offset = address - 0x200000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x800000 && address + 1 <= 0x800001)
            {
                result = (short)Taito.TC0220IOC_halfword_byteswap_portreg_r();
            }
            else if (address >= 0x800002 && address + 1 <= 0x800003)
            {
                result = (short)Taito.TC0220IOC_halfword_byteswap_port_r();
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa00001)
            {
                result = 0;
            }
            else if (address >= 0xa00002 && address + 1 <= 0xa00003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            return result;
        }
        public static int MReadOpLong_masterw(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    result = (int)(Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3]);
                }
                else
                {
                    result = 0;
                }
            }
            else if(address>=0x200000&&address+3<=0x203fff)
            {
                int offset=address-0x200000;
                result = (int)(Memory.mainram[offset] * 0x1000000 + Memory.mainram[offset + 1] * 0x10000 + Memory.mainram[offset + 2] * 0x100 + Memory.mainram[offset + 3]);
            }
            return result;
        }
        public static int MReadLong_masterw(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    result = (int)(Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x200000 && address + 3 <= 0x203fff)
            {
                int offset = address - 0x200000;
                result = (int)(Memory.mainram[offset] * 0x1000000 + Memory.mainram[offset + 1] * 0x10000 + Memory.mainram[offset + 2] * 0x100 + Memory.mainram[offset + 3]);
            }
            else if (address >= 0x400000 && address + 3 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (int)(TC0180VCU_word_r(offset) * 0x10000 + TC0180VCU_word_r(offset + 1));
            }
            else if (address >= 0x410000 && address + 3 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (int)(taitob_spriteram[offset] * 0x10000 + taitob_spriteram[offset + 1]);
            }
            else if (address >= 0x411980 && address + 3 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                result = (int)(mainram2[offset] * 0x1000000 + mainram2[offset + 1] * 0x10000 + mainram2[offset + 2] * 0x100 + mainram2[offset + 3]);
            }
            else if (address >= 0x413800 && address + 3 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (int)(taitob_scroll[offset] * 0x10000 + taitob_scroll[offset + 1]);
            }
            else if (address >= 0x418000 && address + 3 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (int)(taitob_v_control_r(offset) * 0x10000 + taitob_v_control_r(offset + 1));
            }
            else if (address >= 0x440000 && address + 3 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (int)(TC0180VCU_framebuffer_word_r(offset) * 0x10000 + TC0180VCU_framebuffer_word_r(offset + 1));
            }
            else if (address >= 0x600000 && address + 3 <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                result = (int)(Generic.paletteram16[offset] * 0x10000 + Generic.paletteram16[offset + 1]);
            }
            return result;
        }
        public static void MWriteByte_masterw(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x203fff)
            {
                int offset = address - 0x200000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x800001)
            {
                if (address % 2 == 0)
                {
                    Taito.TC0220IOC_halfword_byteswap_portreg_w1((byte)value);
                }
            }
            else if (address >= 0x800002 && address <= 0x800003)
            {
                if (address % 2 == 0)
                {
                    Taito.TC0220IOC_halfword_byteswap_port_w1((byte)value);
                }
            }
            else if (address >= 0xa00000 && address <= 0xa00001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0xa00002 && address <= 0xa00003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
        }
        public static void MWriteWord_masterw(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x203fff)
            {
                int offset = address - 0x200000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x800001)
            {
                Taito.TC0220IOC_halfword_byteswap_portreg_w((ushort)value);
            }
            else if (address >= 0x800002 && address + 1 <= 0x800003)
            {
                Taito.TC0220IOC_halfword_byteswap_port_w((ushort)value);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa00001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0xa00002 && address + 1 <= 0xa00003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
        }
        public static void MWriteLong_masterw(int address, int value)
        {
            MWriteWord_masterw(address, (short)(value >> 16));
            MWriteWord_masterw(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_rastsag2(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x600000 && address <= 0x607fff)
            {
                int offset = address - 0x600000;
                result=(sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_rastsag2(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x201fff)
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
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x607fff)
            {
                int offset = address - 0x600000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x800000 && address <= 0x800001)
            {
                result = 0;
            }
            else if (address >= 0x800002 && address <= 0x800003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0xa00000 && address <= 0xa0000f)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            return result;
        }
        public static short MReadOpWord_rastsag2(int address)
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
            else if (address >= 0x600000 && address + 1 <= 0x607fff)
            {
                int offset = address - 0x600000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_rastsag2(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x607fff)
            {
                int offset = address - 0x600000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x800000 && address + 1 <= 0x800001)
            {
                result = 0;
            }
            else if (address >= 0x800002 && address + 1 <= 0x800003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa0000f)
            {
                int offset = (address - 0xa0000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            return result;
        }
        public static int MReadOpLong_rastsag2(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    result = (int)(Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x600000 && address + 3 <= 0x607fff)
            {
                int offset = address - 0x600000;
                result = (int)(Memory.mainram[offset] * 0x1000000 + Memory.mainram[offset + 1] * 0x10000 + Memory.mainram[offset + 2] * 0x100 + Memory.mainram[offset + 3]);
            }
            return result;
        }
        public static int MReadLong_rastsag2(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    result = (int)(Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x200000 && address + 3 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                result = (int)(Generic.paletteram16[offset] * 0x10000 + Generic.paletteram16[offset + 1]);
            }
            else if (address >= 0x400000 && address + 3 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (int)(TC0180VCU_word_r(offset) * 0x10000 + TC0180VCU_word_r(offset + 1));
            }
            else if (address >= 0x410000 && address + 3 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (int)(taitob_spriteram[offset] * 0x10000 + taitob_spriteram[offset + 1]);
            }
            else if (address >= 0x411980 && address + 3 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                result = (int)(mainram2[offset] * 0x1000000 + mainram2[offset + 1] * 0x10000 + mainram2[offset + 2] * 0x100 + mainram2[offset + 3]);
            }
            else if (address >= 0x413800 && address + 3 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (int)(taitob_scroll[offset] * 0x10000 + taitob_scroll[offset + 1]);
            }
            else if (address >= 0x418000 && address + 3 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (int)(taitob_v_control_r(offset) * 0x10000 + taitob_v_control_r(offset + 1));
            }
            else if (address >= 0x440000 && address + 3 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (int)(TC0180VCU_framebuffer_word_r(offset) * 0x10000 + TC0180VCU_framebuffer_word_r(offset + 1));
            }
            else if (address >= 0x600000 && address + 3 <= 0x607fff)
            {
                int offset = address - 0x600000;
                result = (int)(Memory.mainram[offset] * 0x1000000 + Memory.mainram[offset + 1] * 0x10000 + Memory.mainram[offset + 2] * 0x100 + Memory.mainram[offset + 3]);
            }
            else if (address >= 0xa00000 && address + 3 <= 0xa0000f)
            {
                int offset = (address - 0xa00000) / 2;
                result = (int)(Taito.TC0220IOC_halfword_byteswap_r(offset) * 0x10000 + Taito.TC0220IOC_halfword_byteswap_r(offset + 1));
            }
            return result;
        }
        public static void MWriteByte_rastsag2(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x607fff)
            {
                int offset = address - 0x600000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x800000 && address <= 0x800001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x800002 && address <= 0x800003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0xa00000 && address <= 0xa0000f)
            {
                int offset = (address - 0xa00000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
        }
        public static void MWriteWord_rastsag2(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x607fff)
            {
                int offset = address - 0x600000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x800000 && address + 1 <= 0x800001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x800002 && address + 1 <= 0x800003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa0000f)
            {
                int offset = (address - 0xa00000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_rastsag2(int address, int value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 3 <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)(value >> 24);
                    Memory.mainrom[address + 1] = (byte)(value >> 16);
                    Memory.mainrom[address + 2] = (byte)(value >> 8);
                    Memory.mainrom[address + 3] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address + 3 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)(value >> 16));
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x400000 && address + 3 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)(value >> 16));
                TC0180VCU_word_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x410000 && address + 3 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)(value >> 16);
                taitob_spriteram[offset + 1] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 3 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 24);
                mainram2[offset + 1] = (byte)(value >> 16);
                mainram2[offset + 2] = (byte)(value >> 8);
                mainram2[offset + 3] = (byte)value;
            }
            else if (address >= 0x413800 && address + 3 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)(value >> 16);
                taitob_scroll[offset + 1] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 3 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)(value >> 16));
                taitob_v_control_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x440000 && address + 3 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)(value >> 16));
                TC0180VCU_framebuffer_word_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x600000 && address + 3 <= 0x607fff)
            {
                int offset = address - 0x600000;
                Memory.mainram[offset] = (byte)(value >> 24);
                Memory.mainram[offset + 1] = (byte)(value >> 16);
                Memory.mainram[offset + 2] = (byte)(value >> 8);
                Memory.mainram[offset + 3] = (byte)value;
            }
            else if (address >= 0xa00000 && address + 3 <= 0xa0000f)
            {
                int offset = (address - 0xa00000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)(value >> 16));
                Taito.TC0220IOC_halfword_byteswap_w(offset + 1, (ushort)value);
            }
        }
        public static sbyte MReadOpByte_rambo3(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_rambo3(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x200001)
            {
                result = 0;
            }
            else if (address >= 0x200002 && address <= 0x200003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0x600010 && address <= 0x600011)
            {
                result = (sbyte)tracky1_lo_r();
            }
            else if (address >= 0x600012 && address <= 0x600013)
            {
                result = (sbyte)tracky1_hi_r();
            }
            else if (address >= 0x600014 && address <= 0x600015)
            {
                result = (sbyte)trackx1_lo_r();
            }
            else if (address >= 0x600016 && address <= 0x600017)
            {
                result = (sbyte)trackx1_hi_r();
            }
            else if (address >= 0x600018 && address <= 0x600019)
            {
                result = (sbyte)tracky2_lo_r();
            }
            else if (address >= 0x60001a && address <= 0x60001b)
            {
                result = (sbyte)tracky2_hi_r();
            }
            else if (address >= 0x60001c && address <= 0x60001d)
            {
                result = (sbyte)trackx2_lo_r();
            }
            else if (address >= 0x60001e && address <= 0x60001f)
            {
                result = (sbyte)trackx2_hi_r();
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0xa00000 && address <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            return result;
        }
        public static short MReadOpWord_rambo3(int address)
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
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_rambo3(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x200001)
            {
                result = 0;
            }
            else if (address >= 0x200002 && address + 1 <= 0x200003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0x600010 && address + 1 <= 0x600011)
            {
                result = (sbyte)tracky1_lo_r();
            }
            else if (address >= 0x600012 && address + 1 <= 0x600013)
            {
                result = (sbyte)tracky1_hi_r();
            }
            else if (address >= 0x600014 && address + 1 <= 0x600015)
            {
                result = (sbyte)trackx1_lo_r();
            }
            else if (address >= 0x600016 && address + 1 <= 0x600017)
            {
                result = (sbyte)trackx1_hi_r();
            }
            else if (address >= 0x600018 && address + 1 <= 0x600019)
            {
                result = (sbyte)tracky2_lo_r();
            }
            else if (address >= 0x60001a && address + 1 <= 0x60001b)
            {
                result = (sbyte)tracky2_hi_r();
            }
            else if (address >= 0x60001c && address + 1 <= 0x60001d)
            {
                result = (sbyte)trackx2_lo_r();
            }
            else if (address >= 0x60001e && address + 1 <= 0x60001f)
            {
                result = (sbyte)trackx2_hi_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            return result;
        }
        public static int MReadOpLong_rambo3(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_rambo3(address) * 0x10000 + (ushort)MReadOpWord_rambo3(address + 2));
            return result;
        }
        public static int MReadLong_rambo3(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_rambo3(address) * 0x10000 + (ushort)MReadWord_rambo3(address + 2));
            return result;
        }
        public static void MWriteByte_rambo3(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x200001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x200002 && address <= 0x200003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0xa00000 && address <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
        }
        public static void MWriteWord_rambo3(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x200001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x200002 && address + 1 <= 0x200003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_rambo3(int address, int value)
        {
            MWriteWord_rambo3(address, (short)(value >> 16));
            MWriteWord_rambo3(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_crimec(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0xa00000 && address <= 0xa0ffff)
            {
                int offset = address - 0xa00000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_crimec(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if(address>=0x200000&&address<=0x20000f)
            {
                int offset=(address-0x200000)/2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                result = 0;
            }
            else if (address >= 0x600002 && address <= 0x600003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0xa00000 && address <= 0xa0ffff)
            {
                int offset = address - 0xa00000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short MReadOpWord_crimec(int address)
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
            else if (address >= 0xa00000 && address <= 0xa0ffff)
            {
                int offset = address - 0xa00000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_crimec(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                result = 0;
            }
            else if (address >= 0x600002 && address + 1 <= 0x600003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0xa00000 && address <= 0xa0ffff)
            {
                int offset = address - 0xa00000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_crimec(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_crimec(address) * 0x10000 + (ushort)MReadOpWord_crimec(address + 2));
            return result;
        }
        public static int MReadLong_crimec(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_crimec(address) * 0x10000 + (ushort)MReadWord_crimec(address + 2));
            return result;
        }
        public static void MWriteByte_crimec(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x600002 && address <= 0x600003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0xa00000 && address <= 0xa0ffff)
            {
                int offset = address - 0xa00000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_crimec(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x600002 && address + 1 <= 0x600003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa0ffff)
            {
                int offset = address - 0xa00000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_crimec(int address,int value)
        {
            MWriteWord_crimec(address, (short)(value >> 16));
            MWriteWord_crimec(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_tetrist(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x800000 && address <= 0x807fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_tetrist(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x200001)
            {
                result = 0;
            }
            else if (address >= 0x200002 && address <= 0x200003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0x800000 && address <= 0x807fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0xa00000 && address <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            return result;
        }
        public static short MReadOpWord_tetrist(int address)
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
            else if (address >= 0x800000 && address + 1 <= 0x807fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_tetrist(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x200001)
            {
                result = 0;
            }
            else if (address >= 0x200002 && address + 1 <= 0x200003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0x800000 && address + 1 <= 0x807fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            return result;
        }
        public static int MReadOpLong_tetrist(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_tetrist(address) * 0x10000 + (ushort)MReadOpWord_tetrist(address + 2));
            return result;
        }
        public static int MReadLong_tetrist(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_tetrist(address) * 0x10000 + (ushort)MReadWord_tetrist(address + 2));
            return result;
        }
        public static void MWriteByte_tetrist(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x200001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x200002 && address <= 0x200003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x800000 && address <= 0x807fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0xa00000 && address <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
        }
        public static void MWriteWord_tetrist(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x200001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x200002 && address + 1 <= 0x200003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x807fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_tetrist(int address, int value)
        {
            MWriteWord_tetrist(address, (short)(value >> 16));
            MWriteWord_tetrist(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_tetrista(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_tetrista(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x201fff)
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
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_portreg_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_portreg_r();
                }
            }
            else if (address >= 0x600002 && address <= 0x600003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_port_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_port_r();
                }
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0xa00000 && address <= 0xa00001)
            {
                result = 0;
            }
            else if (address >= 0xa00002 && address <= 0xa00003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            return result;
        }
        public static short MReadOpWord_tetrista(int address)
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
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_tetrista(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                result = (short)Taito.TC0220IOC_halfword_byteswap_portreg_r();
            }
            else if (address >= 0x600002 && address + 1 <= 0x600003)
            {
                result = (short)Taito.TC0220IOC_halfword_byteswap_port_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa00001)
            {
                result = 0;
            }
            else if (address >= 0xa00002 && address + 1 <= 0xa00003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            return result;
        }
        public static int MReadOpLong_tetrista(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_tetrista(address) * 0x10000 + (ushort)MReadOpWord_tetrista(address + 2));
            return result;
        }
        public static int MReadLong_tetrista(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_tetrista(address) * 0x10000 + (ushort)MReadWord_tetrista(address + 2));
            return result;
        }
        public static void MWriteByte_tetrista(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                if (address % 2 == 0)
                {
                    Taito.TC0220IOC_halfword_byteswap_portreg_w1((byte)value);
                }
            }
            else if (address >= 0x600002 && address <= 0x600003)
            {
                if (address % 2 == 0)
                {
                    Taito.TC0220IOC_halfword_byteswap_port_w1((byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0xa00000 && address <= 0xa00001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0xa00002 && address <= 0xa00003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
        }
        public static void MWriteWord_tetrista(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                Taito.TC0220IOC_halfword_byteswap_portreg_w((ushort)value);
            }
            else if (address >= 0x600002 && address + 1 <= 0x600003)
            {
                Taito.TC0220IOC_halfword_byteswap_port_w((ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa00001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0xa00002 && address + 1 <= 0xa00003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
        }
        public static void MWriteLong_tetrista(int address, int value)
        {
            MWriteWord_tetrista(address, (short)(value >> 16));
            MWriteWord_tetrista(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_viofight(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0xa00000 && address <= 0xa03fff)
            {
                int offset = address - 0xa00000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_viofight(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x200001)
            {
                result = 0;
            }
            else if (address >= 0x200002 && address <= 0x200003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x800000 && address <= 0x80000f)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0xa00000 && address <= 0xa03fff)
            {
                int offset = address - 0xa00000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short MReadOpWord_viofight(int address)
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
            else if (address >= 0xa00000 && address + 1 <= 0xa03fff)
            {
                int offset = address - 0xa00000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_viofight(int address)
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
            else if (address >= 0x200000 && address + 1 <= 0x200001)
            {
                result = 0;
            }
            else if (address >= 0x200002 && address + 1 <= 0x200003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x800000 && address + 1 <= 0x80000f)
            {
                int offset = (address - 0x80000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa03fff)
            {
                int offset = address - 0xa00000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_viofight(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_viofight(address) * 0x10000 + (ushort)MReadOpWord_viofight(address + 2));
            return result;
        }
        public static int MReadLong_viofight(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_viofight(address) * 0x10000 + (ushort)MReadWord_viofight(address + 2));
            return result;
        }
        public static void MWriteByte_viofight(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x200001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x200002 && address <= 0x200003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x80000f)
            {
                int offset = (address - 0x800000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0xa00000 && address <= 0xa03fff)
            {
                int offset = address - 0xa00000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_viofight(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x200001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x200002 && address + 1 <= 0x200003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x601fff)
            {
                int offset = (address - 0x600000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x80000f)
            {
                int offset = (address - 0x800000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa03fff)
            {
                int offset = address - 0xa00000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_viofight(int address, int value)
        {
            MWriteWord_viofight(address, (short)(value >> 16));
            MWriteWord_viofight(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_hitice(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_hitice(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0x610000 && address <= 0x610001)
            {
                result = -1;
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                result = 0;
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0xa00000 && address <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0xb00000 && address <= 0xb7ffff)
            {
                int offset = (address - 0xb00000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_pixelram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_pixelram[offset];
                }
            }            
            return result;
        }
        public static short MReadOpWord_hitice(int address)
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
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_hitice(int address)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0x610000 && address + 1 <= 0x610001)
            {
                result = -1;
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                result = 0;
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0xb00000 && address + 1 <= 0xb7ffff)
            {
                int offset = (address - 0xb00000) / 2;
                result = (short)taitob_pixelram[offset];
            }
            return result;
        }
        public static int MReadOpLong_hitice(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_hitice(address) * 0x10000 + (ushort)MReadOpWord_hitice(address + 2));
            return result;
        }
        public static int MReadLong_hitice(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_hitice(address) * 0x10000 + (ushort)MReadWord_hitice(address + 2));
            return result;
        }
        public static void MWriteByte_hitice(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x803fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0xa00000 && address <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            if (address >= 0xb00000 && address <= 0xb7ffff)
            {
                int offset = (address - 0xb00000) / 2;
                if (address % 2 == 0)
                {
                    hitice_pixelram_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    hitice_pixelram_w2(offset, (byte)value);
                }
            }
            if (address >= 0xbffff2 && address <= 0xbffff5)
            {
                int offset = (address - 0xbffff2) / 2;
                if (address % 2 == 0)
                {
                    hitice_pixel_scroll_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    hitice_pixel_scroll_w2(offset, (byte)value);
                }
            }
        }
        public static void MWriteWord_hitice(int address, short value)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x60000f)
            {
                int offset = (address - 0x600000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x803fff)
            {
                int offset = address - 0x800000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0xa00000 && address + 1 <= 0xa01fff)
            {
                int offset = (address - 0xa00000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0xb00000 && address + 1 <= 0xb7ffff)
            {
                int offset = (address - 0xb00000) / 2;
                hitice_pixelram_w(offset, (ushort)value);
            }
            else if (address >= 0xbffff2 && address + 1 <= 0xbffff5)
            {
                int offset = (address - 0xbffff2) / 2;
                hitice_pixel_scroll_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_hitice(int address, int value)
        {
            MWriteWord_hitice(address, (short)(value >> 16));
            MWriteWord_hitice(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_selfeena(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_selfeena(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x200000 && address <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x210000 && address <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x211980 && address <= 0x2137ff)
            {
                result = (sbyte)mainram2[address - 0x211980];
            }
            else if (address >= 0x213800 && address <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x218000 && address <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x240000 && address <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x300000 && address <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x400000 && address <= 0x40000f)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41000f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_byteswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_byteswap_r(offset);
                }
            }
            else if (address >= 0x500000 && address <= 0x500001)
            {
                result = 0;
            }
            else if (address >= 0x500002 && address <= 0x500003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            return result;
        }
        public static short MReadOpWord_selfeena(int address)
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
            else if (address >= 0x100000 && address + 1 <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_selfeena(int address)
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
            else if (address >= 0x100000 && address + 1 <= 0x103fff)
            {
                int offset = address - 0x100000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x200000 && address + 1 <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x210000 && address + 1 <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x211980 && address + 1 <= 0x2137ff)
            {
                int offset = address - 0x210000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x213800 && address + 1 <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x218000 && address + 1 <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x240000 && address + 1 <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x300000 && address + 1 <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x400000 && address + 1 <= 0x40000f)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41000f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)Taito.TC0220IOC_halfword_byteswap_r(offset);
            }
            else if (address >= 0x500000 && address + 1 <= 0x500001)
            {
                result = 0;
            }
            else if (address >= 0x500002 && address + 1 <= 0x500003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            return result;
        }
        public static int MReadOpLong_selfeena(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_selfeena(address) * 0x10000 + (ushort)MReadOpWord_selfeena(address + 2));
            return result;
        }
        public static int MReadLong_selfeena(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_selfeena(address) * 0x10000 + (ushort)MReadWord_selfeena(address + 2));
            return result;
        }
        public static void MWriteByte_selfeena(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x103fff)
            {
                int offset = address - 0x100000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x200000 && address <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x210000 && address <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x211980 && address <= 0x2137ff)
            {
                int offset = address - 0x211980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x213800 && address <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x218000 && address <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x240000 && address <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x300000 && address <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40000f)
            {
                int offset = (address - 0x400000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x410000 && address <= 0x41000f)
            {
                int offset = (address - 0x410000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x500000 && address <= 0x500001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x500002 && address <= 0x500003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
        }
        public static void MWriteWord_selfeena(int address, short value)
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
            else if (address >= 0x100000 && address + 1 <= 0x103fff)
            {
                int offset = address - 0x100000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x200000 && address + 1 <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x210000 && address + 1 <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x211980 && address + 1 <= 0x2137ff)
            {
                int offset = address - 0x211980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x213800 && address + 1 <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x218000 && address + 1 <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x240000 && address + 1 <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x300000 && address + 1 <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40000f)
            {
                int offset = (address - 0x400000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41000f)
            {
                int offset = (address - 0x410000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x500000 && address + 1 <= 0x500001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x500002 && address + 1 <= 0x500003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
        }
        public static void MWriteLong_selfeena(int address, int value)
        {
            MWriteWord_selfeena(address, (short)(value >> 16));
            MWriteWord_selfeena(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_qzshowby(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x0fffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_qzshowby(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x0fffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x200000 && address <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(pbobble_input_bypass_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = 0;
                }
            }
            else if (address >= 0x200024 && address <= 0x200025)
            {
                result = -1;
            }
            else if (address >= 0x200028 && address <= 0x200029)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(player_34_coin_ctrl_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)player_34_coin_ctrl_r();
                }
            }
            else if (address >= 0x20002e && address <= 0x20002f)
            {
                result = -1;
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                result = 0;
            }
            else if (address >= 0x600002 && address <= 0x600003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short MReadOpWord_qzshowby(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x0fffff)
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
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_qzshowby(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x0fffff)
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
            else if (address >= 0x200000 && address + 1 <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)pbobble_input_bypass_r(offset);
            }
            else if (address >= 0x200024 && address + 1 <= 0x200025)
            {
                result = -1;
            }
            else if (address >= 0x200028 && address + 1 <= 0x200029)
            {
                result = (short)player_34_coin_ctrl_r();
            }
            else if (address >= 0x20002e && address + 1 <= 0x20002f)
            {
                result = -1;
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                result = 0;
            }
            else if (address >= 0x600002 && address + 1 <= 0x600003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_qzshowby(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_qzshowby(address) * 0x10000 + (ushort)MReadOpWord_qzshowby(address + 2));
            return result;
        }
        public static int MReadLong_qzshowby(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_qzshowby(address) * 0x10000 + (ushort)MReadWord_qzshowby(address + 2));
            return result;
        }
        public static void MWriteByte_qzshowby(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x200000 && address <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                Taito.TC0220IOC_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x200026 && address <= 0x200027)
            {
                if (address % 2 == 0)
                {
                    eeprom_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    eeprom_w2((byte)value);
                }
            }
            else if (address >= 0x200028 && address <= 0x200029)
            {
                if (address % 2 == 0)
                {
                    player_34_coin_ctrl_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    player_34_coin_ctrl_w2((byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x600001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x600002 && address <= 0x600003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x700000 && address <= 0x700003)
            {
                int offset = (address - 0x700000) / 2;
                if (address % 2 == 0)
                {
                    gain_control_w1(offset, (byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_qzshowby(int address, short value)
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
            else if (address >= 0x200000 && address + 1 <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                Taito.TC0640FIO_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x200026 && address + 1 <= 0x200027)
            {
                eeprom_w((ushort)value);
            }
            else if (address >= 0x200028 && address + 1 <= 0x200029)
            {
                player_34_coin_ctrl_w((ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x600002 && address + 1 <= 0x600003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x700000 && address + 1 <= 0x700003)
            {
                int offset = (address - 0x700000) / 2;
                gain_control_w(offset, (ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_qzshowby(int address, int value)
        {
            MWriteWord_qzshowby(address, (short)(value >> 16));
            MWriteWord_qzshowby(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_pbobble(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset=address-0x900000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_pbobble(int address)
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
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x500000 && address <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(pbobble_input_bypass_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = 0;
                }
            }
            else if (address >= 0x500024 && address <= 0x500025)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = Taito.sbyte3;
                }
            }
            else if (address >= 0x500026 && address <= 0x500027)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(eep_latch_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)eep_latch_r();
                }
            }
            else if (address >= 0x50002e && address <= 0x50002f)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = Taito.sbyte4;
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                result = 0;//NOP
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                result = (sbyte)Memory.mainram[address - 0x900000];
            }
            return result;
        }
        public static short MReadOpWord_pbobble(int address)
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
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_pbobble(int address)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x500000 && address + 1 <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                result = (short)pbobble_input_bypass_r(offset);
            }
            else if (address >= 0x500024 && address + 1 <= 0x500025)
            {
                result = (short)Taito.sbyte3;
            }
            else if (address >= 0x500026 && address + 1 <= 0x500027)
            {
                result = (short)eep_latch_r();
            }
            else if (address >= 0x50002e && address + 1 <= 0x50002f)
            {
                result = (short)Taito.sbyte4;
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                result = 0;//NOP
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset=address-0x900000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_pbobble(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    result = (int)(Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x800000 && address + 3 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (int)(Generic.paletteram16[offset] * 0x10000 + Generic.paletteram16[offset + 1]);
            }
            else if (address >= 0x900000 && address + 3 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (int)(Memory.mainram[offset] * 0x1000000 + Memory.mainram[offset + 1] * 0x10000 + Memory.mainram[offset + 2] * 0x100 + Memory.mainram[offset + 3]);
            }
            return result;
        }
        public static int MReadLong_pbobble(int address)
        {
            address &= 0xffffff;
            int result = 0;
            if (address <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    result = (int)(Memory.mainrom[address] * 0x1000000 + Memory.mainrom[address + 1] * 0x10000 + Memory.mainrom[address + 2] * 0x100 + Memory.mainrom[address + 3]);
                }
                else
                {
                    result = 0;
                }
            }
            else if (address >= 0x400000 && address + 3 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (int)(TC0180VCU_word_r(offset) * 0x10000 + TC0180VCU_word_r(offset + 1));
            }
            else if (address >= 0x410000 && address + 3 <= 0x41197f)
            {
                int offset=(address-0x410000)/2;
                result=(int)(taitob_spriteram[offset]*0x10000+taitob_spriteram[offset+1]);
            }
            else if (address >= 0x411980 && address + 3 <= 0x4137ff)
            {
                int offset=address-0x411980;
                result = (int)(mainram2[offset] * 0x1000000 + mainram2[offset + 1] * 0x10000 + mainram2[offset + 2] * 0x100 + mainram2[offset + 3]);
            }
            else if (address >= 0x413800 && address + 3 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (int)(taitob_scroll[offset] * 0x10000 + taitob_scroll[offset + 1]);
            }
            else if (address >= 0x418000 && address + 3 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (int)(taitob_v_control_r(offset) * 0x10000 + taitob_v_control_r(offset + 1));
            }
            else if (address >= 0x440000 && address + 3 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (int)(TC0180VCU_framebuffer_word_r(offset) * 0x10000 + TC0180VCU_framebuffer_word_r(offset + 1));
            }
            else if (address >= 0x500000 && address + 3 <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                result = (int)(pbobble_input_bypass_r(offset) * 0x10000 + pbobble_input_bypass_r(offset + 1));
            }
            else if (address >= 0x800000 && address + 3 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (int)(Generic.paletteram16[offset] * 0x10000 + Generic.paletteram16[offset + 1]);
            }
            else if (address >= 0x900000 && address + 3 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (int)(Memory.mainram[offset] * 0x1000000 + Memory.mainram[offset + 1] * 0x10000 + Memory.mainram[offset + 2] * 0x100 + Memory.mainram[offset + 3]);
            }
            return result;
        }        
        public static void MWriteByte_pbobble(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000)/2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x500000 && address <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                Taito.TC0640FIO_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x500026 && address <= 0x500027)
            {
                if (address % 2 == 0)
                {
                    eeprom_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    eeprom_w2((byte)value);
                }
            }
            else if (address >= 0x500028 && address <= 0x500029)
            {
                if (address % 2 == 0)
                {
                    player_34_coin_ctrl_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    player_34_coin_ctrl_w2((byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x600003)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    gain_control_w1(offset, (byte)value);
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_pbobble(int address, short value)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x500000 && address + 1 <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                Taito.TC0640FIO_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x500026 && address + 1 <= 0x500027)
            {
                eeprom_w((ushort)value);
            }
            else if (address >= 0x500028 && address + 1 <= 0x500029)
            {
                player_34_coin_ctrl_w((ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600003)
            {
                int offset = (address - 0x600000) / 2;
                gain_control_w(offset, (ushort)value);
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_pbobble(int address, int value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address + 3 <= 0x07ffff)
            {
                if (address + 3 < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)(value >> 24);
                    Memory.mainrom[address + 1] = (byte)(value>>16);
                    Memory.mainrom[address + 2] = (byte)(value >> 8);
                    Memory.mainrom[address + 3] = (byte)value;
                }
            }
            else if (address >= 0x400000 && address + 3 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)(value>>16));
                TC0180VCU_word_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x410000 && address + 3 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)(value >> 16);
                taitob_spriteram[offset + 1] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 3 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 24);
                mainram2[offset + 1] = (byte)(value>>16);
                mainram2[offset + 2] = (byte)(value >> 8);
                mainram2[offset + 3] = (byte)value;
            }
            else if (address >= 0x413800 && address + 3 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)(value>>16);
                taitob_scroll[offset + 1] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 3 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)(value>>16));
                taitob_v_control_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x440000 && address + 3 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)(value>>16));
                TC0180VCU_framebuffer_word_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x500000 && address + 3 <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                Taito.TC0640FIO_halfword_byteswap_w(offset, (ushort)(value >> 16));
                Taito.TC0640FIO_halfword_byteswap_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x600000 && address + 3 <= 0x600003)
            {
                int offset = (address - 0x600000) / 2;
                gain_control_w(offset, (ushort)(value>>16));
                gain_control_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x800000 && address + 3 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)(value>>16));
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset + 1, (ushort)value);
            }
            else if (address >= 0x900000 && address + 3 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)(value >> 24);
                Memory.mainram[offset + 1] = (byte)(value>>16);
                Memory.mainram[offset + 2] = (byte)(value >> 8);
                Memory.mainram[offset + 3] = (byte)value;
            }
        }
        public static sbyte MReadOpByte_spacedx(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_spacedx(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                result = (sbyte)mainram2[address - 0x411980];
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x500000 && address <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(pbobble_input_bypass_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = 0;
                }
            }
            else if (address >= 0x500024 && address <= 0x500025)
            {
                result = Taito.sbyte3; ;
            }
            else if (address >= 0x500026 && address <= 0x500027)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(eep_latch_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)eep_latch_r();
                }
            }
            else if (address >= 0x50002e && address <= 0x50002f)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = Taito.sbyte4;
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                result = 0;
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static short MReadOpWord_spacedx(int address)
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
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_spacedx(int address)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x410000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x500000 && address + 1 <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                result = (short)pbobble_input_bypass_r(offset);
            }
            else if (address >= 0x500024 && address + 1 <= 0x500025)
            {
                result = (short)Taito.sbyte3;
            }
            else if (address >= 0x500026 && address + 1 <= 0x500027)
            {
                result = (short)eep_latch_r();
            }
            else if (address >= 0x50002e && address + 1 <= 0x50002f)
            {
                result = (short)Taito.sbyte4;
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                result = 0;//NOP
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static int MReadOpLong_spacedx(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_spacedx(address) * 0x10000 + (ushort)MReadOpWord_spacedx(address + 2));
            return result;
        }
        public static int MReadLong_spacedx(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_spacedx(address) * 0x10000 + (ushort)MReadWord_spacedx(address + 2));
            return result;
        }
        public static void MWriteByte_spacedx(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x410000 && address <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x411980 && address <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x413800 && address <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x418000 && address <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x440000 && address <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x500000 && address <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                Taito.TC0640FIO_halfword_byteswap_w1(offset, (byte)value);
            }
            else if (address >= 0x500026 && address <= 0x500027)
            {
                if (address % 2 == 0)
                {
                    eeprom_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    eeprom_w2((byte)value);
                }
            }
            else if (address >= 0x500028 && address <= 0x500029)
            {
                if (address % 2 == 0)
                {
                    player_34_coin_ctrl_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    player_34_coin_ctrl_w2((byte)value);
                }
            }
            else if (address >= 0x600000 && address <= 0x600003)
            {
                int offset = (address - 0x600000) / 2;
                if (address % 2 == 0)
                {
                    gain_control_w1(offset, (byte)value);
                }
            }
            else if (address >= 0x700000 && address <= 0x700001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x700002 && address <= 0x700003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x800000 && address <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)value;
            }
        }
        public static void MWriteWord_spacedx(int address, short value)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = (address - 0x400000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x410000 && address + 1 <= 0x41197f)
            {
                int offset = (address - 0x410000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x411980 && address + 1 <= 0x4137ff)
            {
                int offset = address - 0x411980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x413800 && address + 1 <= 0x413fff)
            {
                int offset = (address - 0x413800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x418000 && address + 1 <= 0x41801f)
            {
                int offset = (address - 0x418000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x440000 && address + 1 <= 0x47ffff)
            {
                int offset = (address - 0x440000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x500000 && address + 1 <= 0x50000f)
            {
                int offset = (address - 0x500000) / 2;
                Taito.TC0640FIO_halfword_byteswap_w(offset, (ushort)value);
            }
            else if (address >= 0x500026 && address + 1 <= 0x500027)
            {
                eeprom_w((ushort)value);
            }
            else if (address >= 0x500028 && address + 1 <= 0x500029)
            {
                player_34_coin_ctrl_w((ushort)value);
            }
            else if (address >= 0x600000 && address + 1 <= 0x600003)
            {
                int offset = (address - 0x600000) / 2;
                gain_control_w(offset, (ushort)value);
            }
            else if (address >= 0x700000 && address + 1 <= 0x700001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x700002 && address + 1 <= 0x700003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x800000 && address + 1 <= 0x801fff)
            {
                int offset = (address - 0x800000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = address - 0x900000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
        }
        public static void MWriteLong_spacedx(int address, int value)
        {
            MWriteWord_spacedx(address, (short)(value >> 16));
            MWriteWord_spacedx(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_spacedxo(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = address - 0x400000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_spacedxo(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x100001)
            {
                result = 0;//NOP
            }
            else if (address >= 0x100002 && address <= 0x100003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x200000 && address <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0220IOC_halfword_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0220IOC_halfword_r(offset);
                }
            }
            else if (address >= 0x210000 && address <= 0x210001)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = Taito.sbyte3;
                }
            }
            else if (address >= 0x220000 && address <= 0x220001)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = Taito.sbyte4;
                }
            }
            else if (address >= 0x230000 && address <= 0x230001)
            {
                if (address % 2 == 0)
                {
                    result = 0;
                }
                else if (address % 2 == 1)
                {
                    result = Taito.sbyte5;
                }
            }
            else if (address >= 0x300000 && address <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Generic.paletteram16[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Generic.paletteram16[offset];
                }
            }
            else if (address >= 0x302000 && address <= 0x303fff)
            {
                result = 0;
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = address - 0x400000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x500000 && address <= 0x50ffff)
            {
                int offset = (address - 0x500000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x510000 && address <= 0x51197f)
            {
                int offset = (address - 0x510000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x511980 && address <= 0x5137ff)
            {
                result = (sbyte)mainram2[address - 0x511980];
            }
            else if (address >= 0x513800 && address <= 0x513fff)
            {
                int offset = (address - 0x513800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x518000 && address <= 0x51801f)
            {
                int offset = (address - 0x518000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x540000 && address <= 0x57ffff)
            {
                int offset = (address - 0x540000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            return result;
        }
        public static short MReadOpWord_spacedxo(int address)
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
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = address - 0x400000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_spacedxo(int address)
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
            else if (address >= 0x100000 && address + 1 <= 0x100001)
            {
                result = 0;
            }
            else if (address >= 0x100002 && address + 1 <= 0x100003)
            {
                result = (short)Taitosnd.taitosound_comm16_msb_r();
            }
            else if (address >= 0x200000 && address + 1 <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Taito.TC0220IOC_halfword_r(offset);
            }
            else if (address >= 0x210000 && address + 1 <= 0x210001)
            {
                result = (short)Taito.sbyte3;
            }
            else if (address >= 0x220000 && address + 1 <= 0x220001)
            {
                result = (short)Taito.sbyte4;
            }
            else if (address >= 0x230000 && address + 1 <= 0x230001)
            {
                result = (short)Taito.sbyte5;
            }
            else if (address >= 0x300000 && address + 1 <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x302000 && address + 1 <= 0x303fff)
            {
                result = 0;
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = address - 0x400000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x500000 && address + 1 <= 0x50ffff)
            {
                int offset = (address - 0x500000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x510000 && address + 1 <= 0x51197f)
            {
                int offset = (address - 0x510000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x511980 && address + 1 <= 0x5137ff)
            {
                int offset = address - 0x510000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x513800 && address + 1 <= 0x513fff)
            {
                int offset = (address - 0x513800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x518000 && address + 1 <= 0x51801f)
            {
                int offset = (address - 0x518000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x540000 && address + 1 <= 0x57ffff)
            {
                int offset = (address - 0x540000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            return result;
        }
        public static int MReadOpLong_spacedxo(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_spacedxo(address) * 0x10000 + (ushort)MReadOpWord_spacedxo(address + 2));
            return result;
        }
        public static int MReadLong_spacedxo(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_spacedxo(address) * 0x10000 + (ushort)MReadWord_spacedxo(address + 2));
            return result;
        }
        public static void MWriteByte_spacedxo(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x100001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x100002 && address <= 0x100003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x200000 && address <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                Taito.TC0220IOC_halfword_w1(offset, (byte)value);
            }
            else if (address >= 0x300000 && address <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x400000 && address <= 0x40ffff)
            {
                int offset = address - 0x400000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x500000 && address <= 0x50ffff)
            {
                int offset = (address - 0x500000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x510000 && address <= 0x51197f)
            {
                int offset = (address - 0x510000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x511980 && address <= 0x5137ff)
            {
                int offset = address - 0x511980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x513800 && address <= 0x513fff)
            {
                int offset = (address - 0x513800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x518000 && address <= 0x51801f)
            {
                int offset = (address - 0x518000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x540000 && address <= 0x57ffff)
            {
                int offset = (address - 0x540000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
        }
        public static void MWriteWord_spacedxo(int address, short value)
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
            else if (address >= 0x100000 && address + 1 <= 0x100001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x100002 && address + 1 <= 0x100003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x200000 && address + 1 <= 0x20000f)
            {
                int offset = (address - 0x200000) / 2;
                Taito.TC0220IOC_halfword_w(offset, (ushort)value);
            }
            else if (address >= 0x300000 && address + 1 <= 0x301fff)
            {
                int offset = (address - 0x300000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBRGBx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x400000 && address + 1 <= 0x40ffff)
            {
                int offset = address - 0x400000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x500000 && address + 1 <= 0x50ffff)
            {
                int offset = (address - 0x500000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x510000 && address + 1 <= 0x51197f)
            {
                int offset = (address - 0x510000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x511980 && address + 1 <= 0x5137ff)
            {
                int offset = address - 0x511980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x513800 && address + 1 <= 0x513fff)
            {
                int offset = (address - 0x513800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x518000 && address + 1 <= 0x51801f)
            {
                int offset = (address - 0x518000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x540000 && address + 1 <= 0x57ffff)
            {
                int offset = (address - 0x540000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_spacedxo(int address, int value)
        {
            MWriteWord_spacedxo(address, (short)(value >> 16));
            MWriteWord_spacedxo(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_sbm(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_sbm(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x200000 && address <= 0x201fff)
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
            else if (address >= 0x300000 && address <= 0x30000f)
            {
                int offset = (address - 0x300000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0510NIO_halfword_wordswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0510NIO_halfword_wordswap_r(offset);
                }
            }
            else if (address >= 0x320000 && address <= 0x320001)
            {
                result = 0;
            }
            else if (address >= 0x320002 && address <= 0x320003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taitosnd.taitosound_comm16_msb_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Taitosnd.taitosound_comm16_msb_r();
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = (address - 0x900000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x910000 && address <= 0x91197f)
            {
                int offset = (address - 0x910000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x911980 && address <= 0x9137ff)
            {
                result = (sbyte)mainram2[address - 0x911980];
            }
            else if (address >= 0x913800 && address <= 0x913fff)
            {
                int offset = (address - 0x913800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x918000 && address <= 0x91801f)
            {
                int offset = (address - 0x918000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x940000 && address <= 0x97ffff)
            {
                int offset = (address - 0x940000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            return result;
        }
        public static short MReadOpWord_sbm(int address)
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
            else if (address >= 0x100000 && address + 1 <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_sbm(int address)
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
            else if (address >= 0x100000 && address + 1 <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x200000 && address + 1 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x300000 && address + 1 <= 0x30000f)
            {
                int offset = (address - 0x300000) / 2;
                result = (short)Taito.TC0510NIO_halfword_wordswap_r(offset);
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = (address - 0x900000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x910000 && address + 1 <= 0x91197f)
            {
                int offset = (address - 0x910000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x911980 && address + 1 <= 0x9137ff)
            {
                int offset = address - 0x910000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x913800 && address + 1 <= 0x913fff)
            {
                int offset = (address - 0x913800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x918000 && address + 1 <= 0x91801f)
            {
                int offset = (address - 0x918000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x940000 && address + 1 <= 0x97ffff)
            {
                int offset = (address - 0x940000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            return result;
        }
        public static int MReadOpLong_sbm(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_sbm(address) * 0x10000 + (ushort)MReadOpWord_sbm(address + 2));
            return result;
        }
        public static int MReadLong_sbm(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_sbm(address) * 0x10000 + (ushort)MReadWord_sbm(address + 2));
            return result;
        }
        public static void MWriteByte_sbm(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x07ffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x10ffff)
            {
                int offset = address - 0x100000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x200000 && address <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x300000 && address <= 0x30000f)
            {
                int offset = (address - 0x300000) / 2;
                Taito.TC0510NIO_halfword_wordswap_w1(offset, (byte)value);
            }
            else if (address >= 0x320000 && address <= 0x320001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x320002 && address <= 0x320003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x900000 && address <= 0x90ffff)
            {
                int offset = (address - 0x900000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x910000 && address <= 0x91197f)
            {
                int offset = (address - 0x910000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x911980 && address <= 0x9137ff)
            {
                int offset = address - 0x911980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x913800 && address <= 0x913fff)
            {
                int offset = (address - 0x913800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x918000 && address <= 0x91801f)
            {
                int offset = (address - 0x918000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x940000 && address <= 0x97ffff)
            {
                int offset = (address - 0x940000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
        }
        public static void MWriteWord_sbm(int address, short value)
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
            else if (address >= 0x100000 && address + 1 <= 0x10ffff)
            {
                int offset = address - 0x100000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x200000 && address + 1 <= 0x201fff)
            {
                int offset = (address - 0x200000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x300000 && address + 1 <= 0x30000f)
            {
                int offset = (address - 0x300000) / 2;
                Taito.TC0510NIO_halfword_wordswap_w(offset, (ushort)value);
            }
            else if (address >= 0x320000 && address + 1 <= 0x320001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x320002 && address + 1 <= 0x320003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x900000 && address + 1 <= 0x90ffff)
            {
                int offset = (address - 0x900000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x910000 && address + 1 <= 0x91197f)
            {
                int offset = (address - 0x910000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x911980 && address + 1 <= 0x9137ff)
            {
                int offset = address - 0x911980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x913800 && address + 1 <= 0x913fff)
            {
                int offset = (address - 0x913800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x918000 && address + 1 <= 0x91801f)
            {
                int offset = (address - 0x918000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x940000 && address + 1 <= 0x97ffff)
            {
                int offset = (address - 0x940000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
        }
        public static void MWriteLong_sbm(int address, int value)
        {
            MWriteWord_sbm(address, (short)(value >> 16));
            MWriteWord_sbm(address + 2, (short)value);
        }
        public static sbyte MReadOpByte_realpunc(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x07ffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Memory.mainram[offset];
            }
            return result;
        }
        public static sbyte MReadByte_realpunc(int address)
        {
            address &= 0xffffff;
            sbyte result = 0;
            if (address <= 0x0fffff)
            {
                result = (sbyte)(Memory.mainrom[address]);
            }
            else if (address >= 0x100000 && address <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (sbyte)Memory.mainram[offset];
            }
            else if (address >= 0x110000 && address <= 0x13ffff)
            {
                int offset = address - 0x110000;
                result = (sbyte)mainram3[offset];
            }
            else if (address >= 0x180000 && address <= 0x18000f)
            {
                int offset = (address - 0x180000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(Taito.TC0510NIO_halfword_wordswap_r(offset) >> 8);
                }
                else
                {
                    result = (sbyte)Taito.TC0510NIO_halfword_wordswap_r(offset);
                }
            }
            else if (address >= 0x188000 && address <= 0x188001)
            {
                result = 0;
            }
            else if (address >= 0x188002 && address <= 0x188003)
            {
                result = 0;
            }
            else if (address >= 0x200000 && address <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_word_r(offset);
                }
            }
            else if (address >= 0x210000 && address <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_spriteram[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_spriteram[offset];
                }
            }
            else if (address >= 0x211980 && address <= 0x2137ff)
            {
                result = (sbyte)mainram2[address - 0x211980];
            }
            else if (address >= 0x213800 && address <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_scroll[offset] >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_scroll[offset];
                }
            }
            else if (address >= 0x218000 && address <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(taitob_v_control_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)taitob_v_control_r(offset);
                }
            }
            else if (address >= 0x240000 && address <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                if (address % 2 == 0)
                {
                    result = (sbyte)(TC0180VCU_framebuffer_word_r(offset) >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)TC0180VCU_framebuffer_word_r(offset);
                }
            }
            else if (address >= 0x300000 && address <= 0x300001)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Hd63484.HD63484_status_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Hd63484.HD63484_status_r();
                }
            }
            else if (address >= 0x300002 && address <= 0x300003)
            {
                if (address % 2 == 0)
                {
                    result = (sbyte)(Hd63484.HD63484_data_r() >> 8);
                }
                else if (address % 2 == 1)
                {
                    result = (sbyte)Hd63484.HD63484_data_r();
                }
            }
            else if (address >= 0x320002 && address <= 0x320003)
            {
                result = 0;
            }
            return result;
        }
        public static short MReadOpWord_realpunc(int address)
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
            else if (address >= 0x100000 && address + 1 <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            return result;
        }
        public static short MReadWord_realpunc(int address)
        {
            address &= 0xffffff;
            short result = 0;
            if (address <= 0x0fffff)
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
            else if (address >= 0x100000 && address + 1 <= 0x10ffff)
            {
                int offset = address - 0x100000;
                result = (short)(Memory.mainram[offset] * 0x100 + Memory.mainram[offset + 1]);
            }
            else if (address >= 0x110000 && address + 1 <= 0x13ffff)
            {
                int offset = address - 0x110000;
                result = (short)(mainram3[offset] * 0x100 + mainram3[offset + 1]);
            }
            else if (address >= 0x180000 && address + 1 <= 0x18000f)
            {
                int offset = (address - 0x180000) / 2;
                result = (short)Taito.TC0510NIO_halfword_wordswap_r(offset);
            }
            else if (address >= 0x188000 && address + 1 <= 0x188001)
            {
                result = 0;
            }
            else if (address >= 0x188002 && address + 1 <= 0x188003)
            {
                result = 0;
            }
            else if (address >= 0x200000 && address + 1 <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                result = (short)TC0180VCU_word_r(offset);
            }
            else if (address >= 0x210000 && address + 1 <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                result = (short)taitob_spriteram[offset];
            }
            else if (address >= 0x211980 && address + 1 <= 0x2137ff)
            {
                int offset = address - 0x210000;
                result = (short)(mainram2[offset] * 0x100 + mainram2[offset + 1]);
            }
            else if (address >= 0x213800 && address + 1 <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                result = (short)(taitob_scroll[offset]);
            }
            else if (address >= 0x218000 && address + 1 <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                result = (short)taitob_v_control_r(offset);
            }
            else if (address >= 0x240000 && address + 1 <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                result = (short)TC0180VCU_framebuffer_word_r(offset);
            }
            else if (address >= 0x280000 && address + 1 <= 0x281fff)
            {
                int offset = (address - 0x280000) / 2;
                result = (short)Generic.paletteram16[offset];
            }
            else if (address >= 0x300000 && address + 1 <= 0x300001)
            {
                result=(short)Hd63484.HD63484_status_r();
            }
            else if (address >= 0x300002 && address + 1 <= 0x300003)
            {
                result=(short)Hd63484.HD63484_data_r();
            }
            else if (address >= 0x320002 && address + 1 <= 0x320003)
            {
                result = 0;
            }
            return result;
        }
        public static int MReadOpLong_realpunc(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadOpWord_realpunc(address) * 0x10000 + (ushort)MReadOpWord_realpunc(address + 2));
            return result;
        }
        public static int MReadLong_realpunc(int address)
        {
            int result = 0;
            result = (int)((ushort)MReadWord_realpunc(address) * 0x10000 + (ushort)MReadWord_realpunc(address + 2));
            return result;
        }
        public static void MWriteByte_realpunc(int address, sbyte value)
        {
            address &= 0xffffff;
            if (address >= 0x000000 && address <= 0x0fffff)
            {
                if (address < Memory.mainrom.Length)
                {
                    Memory.mainrom[address] = (byte)value;
                }
            }
            else if (address >= 0x100000 && address <= 0x10ffff)
            {
                int offset = address - 0x100000;
                Memory.mainram[offset] = (byte)value;
            }
            else if (address >= 0x110000 && address <= 0x13ffff)
            {
                int offset = address - 0x110000;
                mainram3[offset] = (byte)value;
            }
            else if (address >= 0x180000 && address <= 0x18000f)
            {
                int offset = (address - 0x180000) / 2;
                Taito.TC0510NIO_halfword_wordswap_w1(offset, (byte)value);
            }
            else if (address >= 0x184000 && address <= 0x184001)
            {
                if (address % 2 == 0)
                {
                    realpunc_video_ctrl_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    realpunc_video_ctrl_w2((byte)value);
                }
            }
            else if (address >= 0x188000 && address <= 0x188001)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_port16_msb_w1((byte)value);
                }
            }
            else if (address >= 0x188002 && address <= 0x188003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
            else if(address>=0x18c000&&address<=0x18c001)
            {
                realpunc_output_w();
            }
            else if (address >= 0x200000 && address <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x210000 && address <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                if (address % 2 == 0)
                {
                    taitob_spriteram[offset] = (ushort)((value << 8) | (taitob_spriteram[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_spriteram[offset] = (ushort)((taitob_spriteram[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x211980 && address <= 0x2137ff)
            {
                int offset = address - 0x211980;
                mainram2[offset] = (byte)value;
            }
            else if (address >= 0x213800 && address <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                if (address % 2 == 0)
                {
                    taitob_scroll[offset] = (ushort)((value << 8) | (taitob_scroll[offset] & 0xff));
                }
                else if (address % 2 == 1)
                {
                    taitob_scroll[offset] = (ushort)((taitob_scroll[offset] & 0xff00) | (byte)value);
                }
            }
            else if (address >= 0x218000 && address <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                if (address % 2 == 0)
                {
                    taitob_v_control_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    taitob_v_control_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x240000 && address <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                if (address % 2 == 0)
                {
                    TC0180VCU_framebuffer_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    TC0180VCU_framebuffer_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x280000 && address <= 0x281fff)
            {
                int offset = (address - 0x280000) / 2;
                if (address % 2 == 0)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w1(offset, (byte)value);
                }
                else if (address % 2 == 1)
                {
                    Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w2(offset, (byte)value);
                }
            }
            else if (address >= 0x300000 && address <= 0x300001)
            {
                if (address % 2 == 1)
                {
                    Hd63484.HD63484_address_w2((byte)value);
                }
            }
            else if (address >= 0x300002 && address <= 0x300003)
            {
                if (address % 2 == 0)
                {
                    Hd63484.HD63484_data_w1((byte)value);
                }
                else if (address % 2 == 1)
                {
                    Hd63484.HD63484_data_w2((byte)value);
                }
            }
            else if (address >= 0x320002 && address <= 0x320003)
            {
                if (address % 2 == 0)
                {
                    Taitosnd.taitosound_comm16_msb_w1((byte)value);
                }
            }
        }
        public static void MWriteWord_realpunc(int address, short value)
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
            else if (address >= 0x100000 && address + 1 <= 0x10ffff)
            {
                int offset = address - 0x100000;
                Memory.mainram[offset] = (byte)(value >> 8);
                Memory.mainram[offset + 1] = (byte)value;
            }
            else if (address >= 0x110000 && address + 1 <= 0x13ffff)
            {
                int offset = address - 0x110000;
                mainram3[offset] = (byte)(value >> 8);
                mainram3[offset + 1] = (byte)value;
            }
            else if (address >= 0x180000 && address + 1 <= 0x18000f)
            {
                int offset = (address - 0x180000) / 2;
                Taito.TC0510NIO_halfword_wordswap_w(offset, (ushort)value);
            }
            else if (address >= 0x184000 && address + 1 <= 0x184001)
            {
                realpunc_video_ctrl_w((ushort)value);
            }
            else if (address >= 0x188000 && address + 1 <= 0x188001)
            {
                Taitosnd.taitosound_port16_msb_w((ushort)value);
            }
            else if (address >= 0x188002 && address + 1 <= 0x188003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
            else if (address >= 0x18c000 && address + 1 <= 0x18c001)
            {
                realpunc_output_w();
            }
            else if (address >= 0x200000 && address + 1 <= 0x20ffff)
            {
                int offset = (address - 0x200000) / 2;
                TC0180VCU_word_w(offset, (ushort)value);
            }
            else if (address >= 0x210000 && address + 1 <= 0x21197f)
            {
                int offset = (address - 0x210000) / 2;
                taitob_spriteram[offset] = (ushort)value;
            }
            else if (address >= 0x211980 && address + 1 <= 0x2137ff)
            {
                int offset = address - 0x211980;
                mainram2[offset] = (byte)(value >> 8);
                mainram2[offset + 1] = (byte)value;
            }
            else if (address >= 0x213800 && address + 1 <= 0x213fff)
            {
                int offset = (address - 0x213800) / 2;
                taitob_scroll[offset] = (ushort)value;
            }
            else if (address >= 0x218000 && address + 1 <= 0x21801f)
            {
                int offset = (address - 0x218000) / 2;
                taitob_v_control_w(offset, (ushort)value);
            }
            else if (address >= 0x240000 && address + 1 <= 0x27ffff)
            {
                int offset = (address - 0x240000) / 2;
                TC0180VCU_framebuffer_word_w(offset, (ushort)value);
            }
            else if (address >= 0x280000 && address + 1 <= 0x281fff)
            {
                int offset = (address - 0x280000) / 2;
                Generic.paletteram16_RRRRGGGGBBBBxxxx_word_w(offset, (ushort)value);
            }
            else if (address >= 0x300000 && address + 1 <= 0x300001)
            {
                Hd63484.HD63484_address_w((ushort)value);
            }
            else if (address >= 0x300002 && address + 1 <= 0x300002)
            {
                Hd63484.HD63484_data_w((ushort)value);
            }
            else if (address >= 0x320002 && address + 1 <= 0x320003)
            {
                Taitosnd.taitosound_comm16_msb_w((ushort)value);
            }
        }
        public static void MWriteLong_realpunc(int address, int value)
        {
            MWriteWord_realpunc(address, (short)(value >> 16));
            MWriteWord_realpunc(address + 2, (short)value);
        }
        public static byte ZReadOp_masterw(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address & 0x7fff];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public static byte ZReadMemory_masterw(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address & 0x7fff];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                result = Memory.audioram[offset];
            }
            else if (address == 0x9000)
            {
                result = YM2203.ym2203_status_port_0_r();
            }
            else if (address == 0xa001)
            {
                result = Taitosnd.taitosound_slave_comm_r();
            }
            return result;
        }
        public static void ZWriteMemory_masterw(ushort address, byte value)
        {
            if (address <= 0x7fff)
            {
                Memory.audiorom[address] = value;
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                Memory.audioram[offset] = value;
            }
            else if (address == 0x9000)
            {
                YM2203.ym2203_control_port_0_w(value);
            }
            else if (address == 0x9001)
            {
                YM2203.ym2203_write_port_0_w(value);
            }
            else if (address == 0xa000)
            {
                Taitosnd.taitosound_slave_port_w(value);
            }
            else if (address == 0xa001)
            {
                Taitosnd.taitosound_slave_comm_w(value);
            }
        }
        public static byte ZReadOp_pbobble(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address & 0x7fff];
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public static byte ZReadMemory_pbobble(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address & 0x7fff];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                result = Memory.audiorom[basebanksnd + (address & 0x3fff)];
            }
            else if (address >= 0xc000 && address <= 0xdfff)
            {
                result = Memory.audioram[address - 0xc000];
            }
            else if (address >= 0xe000 && address <= 0xe000)
            {
                result = YM2610.F2610.ym2610_read(0);
            }
            else if (address >= 0xe001 && address <= 0xe001)
            {
                result = YM2610.F2610.ym2610_read(1);
            }
            else if (address >= 0xe002 && address <= 0xe002)
            {
                result = YM2610.F2610.ym2610_read(2);
            }
            else if (address >= 0xe200 && address <= 0xe200)
            {

            }
            else if (address >= 0xe201 && address <= 0xe201)
            {
                result = Taitosnd.taitosound_slave_comm_r();
            }
            else if (address >= 0xea00 && address <= 0xea00)
            {

            }
            return result;
        }
        public static void ZWriteMemory_pbobble(ushort address, byte value)
        {
            if (address <= 0x7fff)
            {
                Memory.audiorom[address] = value;
            }
            else if (address >= 0xc000 && address <= 0xdfff)
            {
                Memory.audioram[address - 0xc000] = value;
            }
            else if (address >= 0xe000 && address <= 0xe000)
            {
                YM2610.F2610.ym2610_write(0, value);
            }
            else if (address >= 0xe001 && address <= 0xe001)
            {
                YM2610.F2610.ym2610_write(1, value);
            }
            else if (address >= 0xe002 && address <= 0xe002)
            {
                YM2610.F2610.ym2610_write(2, value);
            }
            else if (address >= 0xe003 && address <= 0xe003)
            {
                YM2610.F2610.ym2610_write(3, value);
            }
            else if (address >= 0xe200 && address <= 0xe200)
            {
                Taitosnd.taitosound_slave_port_w(value);
            }
            else if (address >= 0xe201 && address <= 0xe201)
            {
                Taitosnd.taitosound_slave_comm_w(value);
            }
            else if (address >= 0xe400 && address <= 0xe403)
            {

            }
            else if (address >= 0xe600 && address <= 0xe600)
            {

            }
            else if (address >= 0xee00 && address <= 0xee00)
            {

            }
            else if (address >= 0xf000 && address <= 0xf000)
            {

            }
            else if (address >= 0xf200 && address <= 0xf200)
            {
                bankswitch_w(0, value);
            }
        }
        public static byte ZReadOp_viofight(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address & 0x7fff];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public static byte ZReadMemory_viofight(ushort address)
        {
            byte result = 0;
            if (address <= 0x3fff)
            {
                result = Memory.audiorom[address & 0x7fff];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.audiorom[basebanksnd + offset];
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                result = Memory.audioram[offset];
            }
            else if (address == 0x9000)
            {
                result = YM2203.ym2203_status_port_0_r();
            }
            else if (address == 0xa001)
            {
                result = Taitosnd.taitosound_slave_comm_r();
            }
            else if (address == 0xb000)
            {
                result = (byte)OKI6295.oo1[0].okim6295_status_r();
            }
            return result;
        }
        public static void ZWriteMemory_viofight(ushort address, byte value)
        {
            if (address <= 0x7fff)
            {
                Memory.audiorom[address] = value;
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                Memory.audioram[offset] = value;
            }
            else if (address == 0x9000)
            {
                YM2203.ym2203_control_port_0_w(value);
            }
            else if (address == 0x9001)
            {
                YM2203.ym2203_write_port_0_w(value);
            }
            else if (address == 0xa000)
            {
                Taitosnd.taitosound_slave_port_w(value);
            }
            else if (address == 0xa001)
            {
                Taitosnd.taitosound_slave_comm_w(value);
            }
            else if (address >= 0xb000 && address <= 0xb001)
            {
                OKI6295.oo1[0].okim6295_data_w(value);
            }
        }
        public static byte ZReadHardware(ushort address)
        {
            byte result = 0;
            return result;
        }
        public static void ZWriteHardware(ushort address, byte value)
        {
            
        }
    }
}
