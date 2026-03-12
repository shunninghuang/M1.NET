using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class M72
    {
        public static Tmap bg_tilemap, fg_tilemap, bg_tilemap_large;
    }
    public partial class Tmap
    {
        public void tile_update_m72_bg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int code, code1, attr, color, memindex;
            byte pri, flags;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = M72.m72_videoram2[memindex * 4] + M72.m72_videoram2[memindex * 4 + 1] * 0x100;
            color = M72.m72_videoram2[memindex * 4 + 2];
            attr = M72.m72_videoram2[memindex * 4 + 3];
            if ((attr & 0x01) != 0)
            {
                pri = 2;
            }
            else if ((color & 0x80) != 0)
            {
                pri = 1;
            }
            else
            {
                pri = 0;
            }
            code1 = (code + ((attr & 0x3f) << 8)) % M72.bg_tilemap.total_elements;
            pen_data_offset = code1 * 0x40;
            palette_base = 0x100 + 0x10 * (color & 0x0f);
            flags = (byte)((((color & 0xc0) >> 6) & 3) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(M72.gfx31rom, pen_data_offset, x0, y0, palette_base, 0, pri, flags);
        }
        public void tile_update_m72_bg_rtype2(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int code, code1, attr, color, memindex;
            byte pri, flags;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = M72.m72_videoram2[memindex * 4] + M72.m72_videoram2[memindex * 4 + 1] * 0x100;
            color = M72.m72_videoram2[memindex * 4 + 2];
            attr = M72.m72_videoram2[memindex * 4 + 3];
            if ((attr & 0x01) != 0)
            {
                pri = 2;
            }
            else if ((color & 0x80) != 0)
            {
                pri = 1;
            }
            else
            {
                pri = 0;
            }
            code1 = code % M72.bg_tilemap.total_elements;
            pen_data_offset = code1 * 0x40;
            palette_base = 0x100 + 0x10 * (color & 0x0f);
            flags = (byte)((((color & 0x60) >> 5) & 3) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(M72.gfx21rom, pen_data_offset, x0, y0, palette_base, 0, pri, flags);
        }
        public void tile_update_m72_fg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int code, code1, attr, color, memindex;
            byte pri, flags;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = M72.m72_videoram1[memindex * 4] + M72.m72_videoram1[memindex * 4 + 1] * 0x100;
            color = M72.m72_videoram1[memindex * 4 + 2];
            attr = M72.m72_videoram1[memindex * 4 + 3];
            if ((attr & 0x01) != 0)
            {
                pri = 2;
            }
            else if ((color & 0x80) != 0)
            {
                pri = 1;
            }
            else
            {
                pri = 0;
            }
            code1 = code % M72.fg_tilemap.total_elements;
            pen_data_offset = code1 * 0x40;
            palette_base = 0x100 + 0x10 * (color & 0x0f);
            flags = (byte)((((color & 0x60) >> 5) & 3) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(M72.gfx21rom, pen_data_offset, x0, y0, palette_base, 0, pri, flags);
        }
        public void tile_update_m72_fg_rtype2(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int code, code1, attr, color, memindex;
            byte pri, flags;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code = M72.m72_videoram1[memindex * 4] + M72.m72_videoram1[memindex * 4 + 1] * 0x100;
            color = M72.m72_videoram1[memindex * 4 + 2];
            attr = M72.m72_videoram1[memindex * 4 + 3];
            if ((attr & 0x01) != 0)
            {
                pri = 2;
            }
            else if ((color & 0x80) != 0)
            {
                pri = 1;
            }
            else
            {
                pri = 0;
            }
            code1 = code % M72.fg_tilemap.total_elements;
            pen_data_offset = code1 * 0x40;
            palette_base = 0x100 + 0x10 * (color & 0x0f);
            flags = (byte)((((color & 0x60) >> 5) & 3) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(M72.gfx21rom, pen_data_offset, x0, y0, palette_base, 0, pri, flags);
        }
    }
}
