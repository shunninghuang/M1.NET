using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Gaelco
    {
        public static Tmap[] gaelco_tilemap;
    }
    public partial class Tmap
    {
        public void tile_update_gaelco_screen0(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, color;
            byte category, flags;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            int data = Gaelco.gaelco_videoram[memindex << 1];
            int data2 = Gaelco.gaelco_videoram[(memindex << 1) + 1];
            code = ((data & 0xfffc) >> 2);
            color = data2 & 0x3f;
            pen_data_offset = (0x4000 + code) * 0x100;
            palette_base = 0x10 * color;
            category = (byte)((data2 >> 6) & 0x03);
            flags = (byte)(data & 0x03);
            tileflags[logindex] = tile_draw(Gaelco.gfx2rom, pen_data_offset, x0, y0, palette_base, category, 0, flags);
        }
        public void tile_update_gaelco_screen1(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, color;
            byte category, flags;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            int data = Gaelco.gaelco_videoram[(0x1000 / 2) + (memindex << 1)];
            int data2 = Gaelco.gaelco_videoram[(0x1000 / 2) + (memindex << 1) + 1];
            code = ((data & 0xfffc) >> 2);
            color = data2 & 0x3f;
            pen_data_offset = (0x4000 + code) * 0x100;
            palette_base = 0x10 * color;
            category = (byte)((data2 >> 6) & 0x03);
            flags = (byte)(data & 0x03);
            tileflags[logindex] = tile_draw(Gaelco.gfx2rom, pen_data_offset, x0, y0, palette_base, category, 0, flags);
        }
    }
}
