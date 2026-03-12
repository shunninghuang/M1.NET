using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Technos
    {
        public static byte byte1, byte2, bytee;
        public static byte byte1_old, byte2_old, bytee_old;
        public static byte H0ReadOp_ddragon(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x0fff)
            {
                result = Memory.mainram[address];
            }
            else if (address >= 0x1400 && address <= 0x17ff)
            {
                int offset = address - 0x1400;
                result = mainram2[offset];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.mainrom[basebankmain + offset];
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.mainrom[address];
            }
            return result;
        }
        public static byte H0ReadByte_ddragon(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x0fff)
            {
                result = Memory.mainram[address];
            }
            else if (address >= 0x1000 && address <= 0x11ff)
            {
                int offset = address - 0x1000;
                result = Generic.paletteram[offset];
            }
            else if (address >= 0x1200 && address <= 0x13ff)
            {
                int offset = address - 0x1200;
                result = Generic.paletteram_2[offset];
            }
            else if (address >= 0x1400 && address <= 0x17ff)
            {
                int offset = address - 0x1400;
                result = mainram2[offset];
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                result = ddragon_fgvideoram[offset];
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                result = ddragon_spriteram_r(offset);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                result = ddragon_bgvideoram[offset];
            }
            else if (address == 0x3800)
            {
                result = byte1;
            }
            else if (address == 0x3801)
            {
                result = byte2;
            }
            else if (address == 0x3802)
            {
                result = getbytee();
            }
            else if (address == 0x3803)
            {
                result = dsw0;
            }
            else if (address == 0x3804)
            {
                result = dsw1;
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.mainrom[basebankmain + offset];
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.mainrom[address];
            }
            return result;
        }
        public static byte H0ReadOp_ddragon2(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x17ff)
            {
                result = Memory.mainram[address];
            }
            if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.mainrom[basebankmain + offset];
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.mainrom[address];
            }
            return result;
        }
        public static byte H0ReadByte_ddragon2(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x17ff)
            {
                result = Memory.mainram[address];
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                result = ddragon_fgvideoram[offset];
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                result = ddragon_spriteram_r(offset);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                result = ddragon_bgvideoram[offset];
            }
            else if (address == 0x3800)
            {
                result = byte1;
            }
            else if (address == 0x3801)
            {
                result = byte2;
            }
            else if (address == 0x3802)
            {
                result = getbytee();
            }
            else if (address == 0x3803)
            {
                result = dsw0;
            }
            else if (address == 0x3804)
            {
                result = dsw1;
            }
            else if (address >= 0x3c00 && address <= 0x3dff)
            {
                int offset = address - 0x3c00;
                result = Generic.paletteram[offset];
            }
            else if (address >= 0x3e00 && address <= 0x3fff)
            {
                int offset = address - 0x3e00;
                result = Generic.paletteram_2[offset];
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                result = Memory.mainrom[basebankmain + offset];
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.mainrom[address];
            }
            return result;
        }
        public static byte H0ReadByte_darktowr(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x0fff)
            {
                result = Memory.mainram[address];
            }
            else if (address >= 0x1000 && address <= 0x11ff)
            {
                int offset = address - 0x1000;
                result = Generic.paletteram[offset];
            }
            else if (address >= 0x1200 && address <= 0x13ff)
            {
                int offset = address - 0x1200;
                result = Generic.paletteram_2[offset];
            }
            else if (address >= 0x1400 && address <= 0x17ff)
            {
                int offset = address - 0x1400;
                result = mainram2[offset];
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                result = ddragon_fgvideoram[offset];
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                result = ddragon_spriteram_r(offset);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                result = ddragon_bgvideoram[offset];
            }
            else if (address == 0x3800)
            {
                result = byte1;
            }
            else if (address == 0x3801)
            {
                result = byte2;
            }
            else if (address == 0x3802)
            {
                result = getbytee();
            }
            else if (address == 0x3803)
            {
                result = dsw0;
            }
            else if (address == 0x3804)
            {
                result = dsw1;
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset = address - 0x4000;
                if (darktowr_flag == 1)
                {
                    result = Memory.mainrom[basebankmain + offset];
                }
                else if (darktowr_flag == 2)
                {
                    result = darktowr_mcu_bank_r(offset);
                }
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.mainrom[address];
            }
            return result;
        }
        public static void H0WriteByte_ddragon(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x0fff)
            {
                Memory.mainram[address] = value;
            }
            else if (address >= 0x1000 && address <= 0x11ff)
            {
                int offset = address - 0x1000;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split1_w(offset, value);
            }
            else if (address >= 0x1200 && address <= 0x13ff)
            {
                int offset = address - 0x1200;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split2_w(offset, value);
            }
            else if (address >= 0x1400 && address <= 0x17ff)
            {
                int offset = address - 0x1400;
                mainram2[offset] = value;
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                ddragon_fgvideoram_w(offset, value);
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                ddragon_spriteram_w(offset, value);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                ddragon_bgvideoram_w(offset, value);
            }
            else if (address == 0x3808)
            {
                ddragon_bankswitch_w(value);
            }
            else if (address == 0x3809)
            {
                ddragon_scrollx_lo = value;
            }
            else if (address == 0x380a)
            {
                ddragon_scrolly_lo = value;
            }
            else if (address >= 0x380b && address <= 0x380f)
            {
                int offset = address - 0x380b;
                ddragon_interrupt_w(offset, value);
            }
        }
        public static void H0WriteByte_darktowr(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x0fff)
            {
                Memory.mainram[address] = value;
            }
            else if (address >= 0x1000 && address <= 0x11ff)
            {
                int offset = address - 0x1000;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split1_w(offset, value);
            }
            else if (address >= 0x1200 && address <= 0x13ff)
            {
                int offset = address - 0x1200;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split2_w(offset, value);
            }
            else if (address >= 0x1400 && address <= 0x17ff)
            {
                int offset = address - 0x1400;
                mainram2[offset] = value;
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                ddragon_fgvideoram_w(offset, value);
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                ddragon_spriteram_w(offset, value);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                ddragon_bgvideoram_w(offset, value);
            }
            else if (address == 0x3808)
            {
                darktowr_bankswitch_w(value);
            }
            else if (address == 0x3809)
            {
                ddragon_scrollx_lo = value;
            }
            else if (address == 0x380a)
            {
                ddragon_scrolly_lo = value;
            }
            else if (address >= 0x380b && address <= 0x380f)
            {
                int offset = address - 0x380b;
                ddragon_interrupt_w(offset, value);
            }
            else if (address >= 0x4000 && address <= 0x7fff)
            {
                int offset=address-0x4000;
                if (darktowr_flag == 2)
                {
                    darktowr_mcu_bank_w(offset, value);
                }
            }
        }
        public static void H0WriteByte_toffy(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x0fff)
            {
                Memory.mainram[address] = value;
            }
            else if (address >= 0x1000 && address <= 0x11ff)
            {
                int offset = address - 0x1000;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split1_w(offset, value);
            }
            else if (address >= 0x1200 && address <= 0x13ff)
            {
                int offset = address - 0x1200;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split2_w(offset, value);
            }
            else if (address >= 0x1400 && address <= 0x17ff)
            {
                int offset = address - 0x1400;
                mainram2[offset] = value;
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                ddragon_fgvideoram_w(offset, value);
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                ddragon_spriteram_w(offset, value);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                ddragon_bgvideoram_w(offset, value);
            }
            else if (address == 0x3808)
            {
                toffy_bankswitch_w(value);
            }
            else if (address == 0x3809)
            {
                ddragon_scrollx_lo = value;
            }
            else if (address == 0x380a)
            {
                ddragon_scrolly_lo = value;
            }
            else if (address >= 0x380b && address <= 0x380f)
            {
                int offset = address - 0x380b;
                ddragon_interrupt_w(offset, value);
            }
        }
        public static void H0WriteByte_ddragon2(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x17ff)
            {
                Memory.mainram[address] = value;
            }
            else if (address >= 0x1800 && address <= 0x1fff)
            {
                int offset = address - 0x1800;
                ddragon_fgvideoram_w(offset, value);
            }
            else if (address >= 0x2000 && address <= 0x2fff)
            {
                int offset = address - 0x2000;
                ddragon_spriteram_w(offset, value);
            }
            else if (address >= 0x3000 && address <= 0x37ff)
            {
                int offset = address - 0x3000;
                ddragon_bgvideoram_w(offset, value);
            }
            else if (address == 0x3808)
            {
                ddragon_bankswitch_w(value);
            }
            else if (address == 0x3809)
            {
                ddragon_scrollx_lo = value;
            }
            else if (address == 0x380a)
            {
                ddragon_scrolly_lo = value;
            }
            else if (address >= 0x380b && address <= 0x380f)
            {
                int offset = address - 0x380b;
                ddragon_interrupt_w(offset, value);
            }
            else if (address >= 0x3c00 && address <= 0x3dff)
            {
                int offset = address - 0x3c00;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split1_w(offset, value);
            }
            else if (address >= 0x3e00 && address <= 0x3fff)
            {
                int offset = address - 0x3e00;
                Generic.paletteram_xxxxBBBBGGGGRRRR_split2_w(offset, value);
            }
        }
        public static byte H1ReadOp_ddragon(ushort address)
        {
            byte result = 0;
            if (address >= 0xc000 && address <= 0xffff)
            {
                result = subrom[address];
            }
            return result;
        }
        public static byte Z1ReadOP_ddragon2(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0xbfff)
            {
                result = subrom[address];
            }
            return result;
        }
        public static byte H1ReadByte_ddragon(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x001f)
            {
                result = ddragon_hd63701_internal_registers_r();
            }
            else if (address >= 0x001f && address <= 0x0fff)
            {
                result = subram[address];
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                result = ddragon_spriteram_r(offset);
            }
            else if (address >= 0xc000 && address <= 0xffff)
            {
                result = subrom[address];
            }
            return result;
        }
        public static byte M1ReadByte_ddragonba(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x001f)
            {
                result = cpu.m6800.M6800.m1.m6803_internal_registers_r(address);
            }
            else if (address >= 0x0020 && address <= 0x007f)
            {
                result = 0;
            }
            else if (address >= 0x0080 && address <= 0x0fff)
            {
                result = subram[address];
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                result = ddragon_spriteram_r(offset);
            }
            else if (address >= 0xc000 && address <= 0xffff)
            {
                result = subrom[address];
            }
            return result;
        }
        public static byte Z1ReadByte_ddragon2(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0xbfff)
            {
                result = subrom[address];
            }
            else if (address >= 0xc000 && address <= 0xc3ff)
            {
                int offset = address - 0xc000;
                result = ddragon_spriteram_r(offset);
            }
            return result;
        }
        public static byte M1ReadIO_ddragonba(ushort address)
        {
            return 0;
        }
        public static void M1WriteIO_ddragonba(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0xffff)
            {
                ddragnba_port_w();
            }
        }
        public static void H1WriteByte_ddragon(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x001f)
            {
                ddragon_hd63701_internal_registers_w(address, value);
            }
            else if (address >= 0x001f && address <= 0x0fff)
            {
                subram[address] = value;
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                ddragon_spriteram_w(offset, value);
            }
        }
        public static void M1WriteByte_ddragonba(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x001f)
            {
                cpu.m6800.M6800.m1.m6803_internal_registers_w(address, value);
            }
            else if (address >= 0x0020 && address <= 0x007f)
            {

            }
            else if (address >= 0x0080 && address <= 0x0fff)
            {
                subram[address] = value;
            }
            else if (address >= 0x8000 && address <= 0x8fff)
            {
                int offset = address - 0x8000;
                ddragon_spriteram_w(offset, value);
            }
        }
        public static void Z1WriteByte_ddragon2(ushort address, byte value)
        {
            if (address >= 0xc000 && address <= 0xc3ff)
            {
                int offset = address - 0xc000;
                ddragon_spriteram_w(offset, value);
            }
            else if (address == 0xd000)
            {
                ddragon2_sub_irq_ack_w();
            }
            else if (address == 0xe000)
            {
                ddragon2_sub_irq_w();
            }
        }
        public static byte M2ReadOp_ddragon(ushort address)
        {
            byte result = 0;
            if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.audiorom[address];
            }
            return result;
        }
        public static byte Z2ReadOp_ddragon2(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x7fff)
            {
                result = Memory.audiorom[address];
            }
            return result;
        }
        public static byte M2ReadByte_ddragon(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x0fff)
            {
                result = Memory.audioram[address];
            }
            else if (address == 0x1000)
            {
                result = (byte)Sound.soundlatch_r();
            }
            else if (address == 0x1800)
            {
                result = dd_adpcm_status_r();
            }
            else if (address == 0x2800)
            {
                result = YM2151.ym2151_status_port_0_r();
            }
            else if (address == 0x2801)
            {
                result = YM2151.ym2151_status_port_0_r();
            }
            else if (address >= 0x8000 && address <= 0xffff)
            {
                result = Memory.audiorom[address];
            }
            return result;
        }
        public static byte Z2ReadByte_ddragon2(ushort address)
        {
            byte result = 0;
            if (address >= 0x0000 && address <= 0x7fff)
            {
                result = Memory.audiorom[address];
            }
            else if (address >= 0x8000 && address <= 0x87ff)
            {
                int offset = address - 0x8000;
                result = Memory.audioram[offset];
            }
            else if (address == 0x8801)
            {
                result = YM2151.ym2151_status_port_0_r();
            }
            else if (address == 0x9800)
            {
                result = (byte)OKI6295.oo1[0].okim6295_status_r();
            }
            else if (address == 0xa000)
            {
                result = (byte)Sound.soundlatch_r();
            }
            return result;
        }
        public static void M2WriteByte_ddragon(ushort address, byte value)
        {
            if (address >= 0x0000 && address <= 0x0fff)
            {
                Memory.audioram[address] = value;
            }
            else if (address == 0x2800)
            {
                YM2151.ym2151_register_port_0_w(value);
            }
            else if (address == 0x2801)
            {
                YM2151.ym2151_data_port_0_w(value);
            }
            else if (address >= 0x3800 && address <= 0x3807)
            {
                int offset = address - 0x3800;
                dd_adpcm_w(offset, value);
            }
        }
        public static void Z2WriteByte_ddragon2(ushort address, byte value)
        {
            if (address >= 0x8000 && address <= 0x87ff)
            {
                int offset = address - 0x8000;
                Memory.audioram[offset] = value;
            }
            else if (address == 0x8800)
            {
                YM2151.ym2151_register_port_0_w(value);
            }
            else if (address == 0x8801)
            {
                YM2151.ym2151_data_port_0_w(value);
            }
            else if (address == 0x9800)
            {
                OKI6295.oo1[0].okim6295_data_w(value);
            }
        }
        public static byte M3ReadOp_darktowr(ushort address)
        {
            byte result = 0;
            if (address >= 0x0080 && address <= 0x07ff)
            {
                result = mcurom[address];
            }
            return result;
        }
        public static byte M3ReadByte_darktowr(ushort address)
        {
            byte result = 0;
            address &= 0x7ff;
            if (address >= 0x0000 && address <= 0x0007)
            {
                result = darktowr_mcu_ports[address];
            }
            else if (address >= 0x0008 && address <= 0x007f)
            {
                result = mcuram[address];
            }
            else if (address >= 0x0080 && address <= 0x07ff)
            {
                result = mcurom[address];
            }
            return result;
        }
        public static void M3WriteByte_darktowr(ushort address, byte value)
        {
            address &= 0x7ff;
            if (address >= 0x0000 && address <= 0x0007)
            {
                darktowr_mcu_w(address, value);
            }
            else if (address >= 0x0008 && address <= 0x007f)
            {
                mcuram[address] = value;
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
