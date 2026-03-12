using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Konami68000
    {
        public static Tmap[] K052109_tilemap,K053251_tilemaps;
    }
    public partial class Tmap
    {
        public void tile_update_konami68000_0(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            int flipy = 0;
            memindex = logical_to_memory[logindex];
            code = Konami68000.K052109_ram[Konami68000.K052109_videoram_F_offset + memindex] + 256 * Konami68000.K052109_ram[Konami68000.K052109_videoram2_F_offset + memindex];
            color = Konami68000.K052109_ram[Konami68000.K052109_colorram_F_offset + memindex];
            byte flags = 0;
            int priority = 0;
            int bank = Konami68000.K052109_charrombank[(color & 0x0c) >> 2];
            if (Konami68000.has_extra_video_ram != 0)
            {
                bank = (color & 0x0c) >> 2;
            }
            color = (color & 0xf3) | ((bank & 0x03) << 2);
            bank >>= 2;
            flipy = color & 0x02;
            int code2, color2;
            int flags2;
            Konami68000.K052109_callback(0, bank, code, color, flags, priority, out code2, out color2,out flags2);
            code = code2;
            color = color2;
            flags = (byte)flags2;
            if ((Konami68000.K052109_tileflip_enable & 1) == 0)
            {
                flags &= unchecked((byte)(~0x01));
            }
            if (flipy != 0 && (Konami68000.K052109_tileflip_enable & 2) != 0)
            {
                flags |= 0x02;
            }
            code = code % Konami68000.K052109_tilemap[0].total_elements;
            pen_data_offset = code * 0x40;
            palette_base = 0x10 * color;
            flags = (byte)(flags ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(Konami68000.gfx12rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_konami68000_1(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            int flipy = 0;
            memindex = logical_to_memory[logindex];
            code = Konami68000.K052109_ram[Konami68000.K052109_videoram_A_offset + memindex] + 256 * Konami68000.K052109_ram[Konami68000.K052109_videoram2_A_offset + memindex];
            color = Konami68000.K052109_ram[Konami68000.K052109_colorram_A_offset + memindex];
            byte flags = 0;
            int priority = 0;
            int bank = Konami68000.K052109_charrombank[(color & 0x0c) >> 2];
            if (Konami68000.has_extra_video_ram != 0)
            {
                bank = (color & 0x0c) >> 2;
            }
            color = (color & 0xf3) | ((bank & 0x03) << 2);
            bank >>= 2;
            flipy = color & 0x02;
            int code2, color2,flags2;
            Konami68000.K052109_callback(1, bank, code, color, flags, priority, out code2, out color2,out flags2);
            code = code2;
            color = color2;
            flags = (byte)flags2;
            if ((Konami68000.K052109_tileflip_enable & 1) == 0)
            {
                flags &= unchecked((byte)~0x01);
            }
            if (flipy != 0 && (Konami68000.K052109_tileflip_enable & 2) != 0)
            {
                flags |= 0x02;
            }
            code = code % Konami68000.K052109_tilemap[1].total_elements;
            pen_data_offset = code * 0x40;
            palette_base = 0x10 * color;
            flags = (byte)(flags ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(Konami68000.gfx12rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_konami68000_2(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            int flipy = 0;
            memindex = logical_to_memory[logindex];
            code = Konami68000.K052109_ram[Konami68000.K052109_videoram_B_offset + memindex] + 256 * Konami68000.K052109_ram[Konami68000.K052109_videoram2_B_offset + memindex];
            color = Konami68000.K052109_ram[Konami68000.K052109_colorram_B_offset + memindex];
            byte flags = 0;
            int priority = 0;
            int bank = Konami68000.K052109_charrombank[(color & 0x0c) >> 2];
            if (Konami68000.has_extra_video_ram != 0)
            {
                bank = (color & 0x0c) >> 2;
            }
            color = (color & 0xf3) | ((bank & 0x03) << 2);
            bank >>= 2;
            flipy = color & 0x02;
            int code2,color2,flags2;
            Konami68000.K052109_callback(2, bank, code, color, flags, priority, out code2, out color2,out flags2);
            code = code2;
            color = color2;
            flags = (byte)flags2;
            if ((Konami68000.K052109_tileflip_enable & 1) == 0)
            {
                flags &= unchecked((byte)~0x01);
            }
            if (flipy != 0 && (Konami68000.K052109_tileflip_enable & 2) != 0)
            {
                flags |= 0x02;
            }
            code = code % Konami68000.K052109_tilemap[2].total_elements;
            pen_data_offset = code * 0x40;
            palette_base = 0x10 * color;
            flags = (byte)(flags ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(Konami68000.gfx12rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
    }
}
