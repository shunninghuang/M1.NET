using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Dataeast
    {
        public static Tmap bg_tilemap;
    }
    public partial class Tmap
    {
        public void tile_update_pcktgal_bg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = Generic.videoram[memindex * 2 + 1] + ((Generic.videoram[memindex * 2] & 0x0f) << 8);
            color = Generic.videoram[memindex * 2] >> 4;
            pen_data_offset = code * 0x40;
            palette_base = 0x100 + 0x10 * color;
            tileflags[logindex] = tile_draw(Dataeast.gfx1rom, pen_data_offset, x0, y0, palette_base, 0, 0, 0);
        }
    }
}
