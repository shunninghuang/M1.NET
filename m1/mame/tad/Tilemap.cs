using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Tmap
    {
        public void tile_update_tad_text(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = Generic.videoram16[memindex];
            color = (code >> 12) & 0xf;
            code = code & 0xfff;
            pen_data_offset = code * 0x40;
            palette_base = 0x100 + color * 0x10;
            flags = (byte)(attributes & 0x03);
            tileflags[logindex] = tile_draw(Tad.gfx1rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_tad_back(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = Tad.toki_background1_videoram16[memindex];
            color = (code >> 12) & 0xf;
            code = code & 0xfff;
            pen_data_offset = code * 0x100;
            palette_base = 0x200 + color * 0x10;
            flags = (byte)(attributes & 0x03);
            tileflags[logindex] = tile_draw(Tad.gfx3rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_tad_fore(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = Tad.toki_background2_videoram16[memindex];
            color = (code >> 12) & 0xf;
            code = code & 0xfff;
            pen_data_offset = code * 0x100;
            palette_base = 0x300 + color * 0x10;
            flags = (byte)(attributes & 0x03);
            tileflags[logindex] = tile_draw(Tad.gfx4rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
    }
}
