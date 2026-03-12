using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class PGM
    {
        public static Tmap pgm_tx_tilemap, pgm_bg_tilemap;        
    }
    public partial class Tmap
    {
        public void tile_update_pgm_tx(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int tileno, colour, flipyx;
            int code, memindex, palette_base;
            byte flags;
            memindex = logical_to_memory[logindex];
            tileno = (PGM.pgm_tx_videoram[memindex * 4] * 0x100 + PGM.pgm_tx_videoram[memindex * 4 + 1]) & 0xffff;
            colour = (PGM.pgm_tx_videoram[memindex * 4 + 3] & 0x3e) >> 1;
            flipyx = (PGM.pgm_tx_videoram[memindex * 4 + 3] & 0xc0) >> 6;
            if (tileno > 0xbfff)
            {
                tileno -= 0xc000;
                tileno += 0x20000;
            }
            code = tileno % PGM.pgm_tx_tilemap.total_elements;
            flags = (byte)(flipyx & 3);
            palette_base = 0x800 + 0x10 * colour;
            tileflags[logindex] = tile_draw(PGM.tiles1rom, code * 0x40, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_pgm_bg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int tileno, colour, flipyx;
            int code, memindex, palette_base;
            byte flags;
            memindex = logical_to_memory[logindex];
            tileno = (PGM.pgm_bg_videoram[memindex * 4] * 0x100 + PGM.pgm_bg_videoram[memindex * 4 + 1]) & 0xffff;
            if (tileno > 0x7ff)
            {
                tileno += 0x1000;
            }
            colour = (PGM.pgm_bg_videoram[memindex * 4 + 3] & 0x3e) >> 1;
            flipyx = (PGM.pgm_bg_videoram[memindex * 4 + 3] & 0xc0) >> 6;
            code = tileno % PGM.pgm_bg_tilemap.total_elements;
            flags = (byte)(flipyx & 3);
            palette_base = 0x400 + 0x20 * colour;
            tileflags[logindex] = tile_draw(PGM.tiles2rom, code * 0x400, x0, y0, palette_base, 0, 0, flags);
        }
    }
}
