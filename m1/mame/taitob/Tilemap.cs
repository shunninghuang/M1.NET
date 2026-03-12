using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Taitob
    {
        public static Tmap bg_tilemap, fg_tilemap, tx_tilemap;
    }
    public partial class Tmap
    {
        public void tile_update_taitob_bg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int memindex;
            int tile, code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            tile = Taitob.TC0180VCU_ram[memindex + Taitob.bg_rambank[0]];
            code = tile % total_elements;
            color = Taitob.TC0180VCU_ram[memindex + Taitob.bg_rambank[1]];
            pen_data_offset = code * 0x100;
            palette_base = 0x10 * (Taitob.b_bg_color_base + (color & 0x3f));
            flags = (byte)((((color & 0x00c0) >> 6) & 0x03) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(Taitob.gfx1rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_taitob_fg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int tile_index = 0, memindex;
            int tile, code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            tile = Taitob.TC0180VCU_ram[memindex + Taitob.fg_rambank[0]];
            code = tile % total_elements;
            color = Taitob.TC0180VCU_ram[memindex + Taitob.fg_rambank[1]];
            pen_data_offset = code * 0x100;
            palette_base = 0x10 * (Taitob.b_fg_color_base + (color & 0x3f));
            flags = (byte)((((color & 0x00c0) >> 6) & 0x03) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(Taitob.gfx1rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_taitob_tx(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int tile_index = 0, memindex;
            int tile, code, color;
            int pen_data_offset, palette_base;
            if (attributes == 0)
            {
                tile_index = row * cols + col;
            }
            else if (attributes == 3)
            {
                tile_index = 0x7ff - (row * cols + col);
            }
            else
            {
                int i1 = 1;
            }
            memindex = logical_to_memory[logindex];
            if (memindex != tile_index)
            {
                int i1 = 1;
            }
            tile = Taitob.TC0180VCU_ram[tile_index + Taitob.tx_rambank];
            code = ((tile & 0x07ff) | ((Taitob.TC0180VCU_ctrl[4 + ((tile & 0x800) >> 11)] >> 8) << 11)) % total_elements;
            color = Taitob.b_tx_color_base + ((tile >> 12) & 0x0f);
            pen_data_offset = code * 0x40;
            palette_base = 0x10 * color;
            flags = (byte)(attributes & 0x03);
            tileflags[logindex] = tile_draw(Taitob.gfx0rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
    }
}
