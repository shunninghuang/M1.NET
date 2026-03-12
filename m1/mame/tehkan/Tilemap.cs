using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Tehkan
    {
        public static Tmap bg_tilemap, fg_tilemap;
    }
    public partial class Tmap
    {
        public void tile_update_pbaction_bg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int tile_index,memindex;
            int code, attr, color;
            int pen_data_offset, palette_base;
            tile_index = row * cols + col;
            memindex = logical_to_memory[logindex];
            attr = Generic.colorram[memindex];
            code = Generic.videoram[memindex] + 0x10 * (attr & 0x70);
            color = attr & 0x07;
            flags = (byte)((attr & 0x80) != 0 ? Tilemap.TILE_FLIPY : 0);
            pen_data_offset = code * 0x40;
            palette_base = 0x80 + 0x10 * color;
            tileflags[logindex] = tile_draw(Tehkan.gfx2rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_pbaction_fg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int tile_index,memindex;
            int code, attr, color;
            int pen_data_offset, palette_base;
            tile_index = row * cols + col;
            memindex = logical_to_memory[logindex];
            attr = Tehkan.pbaction_colorram2[memindex];
            code = Tehkan.pbaction_videoram2[memindex] + 0x10 * (attr & 0x30);
            color = attr & 0x0f;
            flags = (byte)(((attr & 0x40) != 0 ? Tilemap.TILE_FLIPX : 0) | ((attr & 0x80) != 0 ? Tilemap.TILE_FLIPY : 0));
            pen_data_offset = code * 0x40;
            palette_base = 0x08 * color;
            tileflags[logindex] = tile_draw(Tehkan.gfx1rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
    }
}
