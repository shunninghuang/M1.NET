using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class CPS
    {
        public static Tmap[] ttmap;
    }
    public partial class Tmap
    {
        public void tile_update_c0(int logindex, int col, int row)
        {
            byte group0, flags0;
            int x0 = 0x08 * col;
            int y0 = 0x08 * row;
            int palette_base0;
            int code, attr;
            int memindex;
            int gfxset;
            int match;
            int i, j;
            memindex = logical_to_memory[logindex];
            code = CPS.gfxram[(CPS.scroll1 + 2 * memindex) * 2] * 0x100 + CPS.gfxram[(CPS.scroll1 + 2 * memindex) * 2 + 1];
            match = 0;
            foreach (CPS.gfx_range r in CPS.lsRange0)
            {
                if (code >= r.start && code <= r.end)
                {
                    code += r.add;
                    match = 1;
                    break;
                }
            }
            code %= CPS.ttmap[0].total_elements;
            gfxset = (memindex & 0x20) >> 5;
            attr = CPS.gfxram[(CPS.scroll1 + 2 * memindex + 1) * 2] * 0x100 + CPS.gfxram[(CPS.scroll1 + 2 * memindex + 1) * 2 + 1];
            if (match == 0)
            {
                Array.Copy(Tilemap.bb0F, 0, pen_data, 0, 0x40);
            }
            else
            {
                for (j = 0; j < 0x08; j++)
                {
                    Array.Copy(CPS.gfx1rom, code * 0x80 + gfxset * 8 + j * 0x10, pen_data, j * 8, 8);
                }
            }
            palette_base0 = 0x10 * ((attr & 0x1f) + 0x20);
            flags0 = (byte)(((attr & 0x60) >> 5) & 3);
            group0 = (byte)((attr & 0x0180) >> 7);
            tileflags[logindex] = tile_draw(pen_data, 0, x0, y0, palette_base0, 0, group0, flags0);
        }
        public void tile_update_c1(int logindex, int col, int row)
        {
            byte group1, flags1;
            int x0 = 0x10 * col;
            int y0 = 0x10 * row;
            int palette_base1;
            int code, attr;
            int memindex;
            int match;
            memindex = logical_to_memory[logindex];
            code = CPS.gfxram[(CPS.scroll2 + 2 * memindex) * 2] * 0x100 + CPS.gfxram[(CPS.scroll2 + 2 * memindex) * 2 + 1];
            match = 0;
            foreach (CPS.gfx_range r in CPS.lsRange1)
            {
                if (code >= r.start && code <= r.end)
                {
                    code += r.add;
                    match = 1;
                    break;
                }
            }
            code %= CPS.ttmap[1].total_elements;
            attr = CPS.gfxram[(CPS.scroll2 + 2 * memindex + 1) * 2] * 0x100 + CPS.gfxram[(CPS.scroll2 + 2 * memindex + 1) * 2 + 1];
            if (match == 0)
            {
                Array.Copy(Tilemap.bb0F, 0, pen_data, 0, 0x100);
            }
            else
            {
                Array.Copy(CPS.gfx1rom, code * 0x100, pen_data, 0, 0x100);
            }
            palette_base1 = 0x10 * ((attr & 0x1f) + 0x40);
            flags1 = (byte)(((attr & 0x60) >> 5) & 3);
            group1 = (byte)((attr & 0x0180) >> 7);
            tileflags[logindex] = tile_draw(pen_data, 0, x0, y0, palette_base1, 0, group1, flags1);
        }
        public void tile_update_c2(int logindex, int col, int row)
        {
            byte group2, flags2;
            int x0 = 0x20 * col;
            int y0 = 0x20 * row;
            int palette_base2;
            int code, attr;
            int memindex;
            int match;
            memindex = logical_to_memory[logindex];
            code = (CPS.gfxram[(CPS.scroll3 + 2 * memindex) * 2] * 0x100 + CPS.gfxram[(CPS.scroll3 + 2 * memindex) * 2 + 1]) & 0x3fff;
            match = 0;
            foreach (CPS.gfx_range r in CPS.lsRange2)
            {
                if (code >= r.start && code <= r.end)
                {
                    code += r.add;
                    match = 1;
                    break;
                }
            }
            code %= CPS.ttmap[2].total_elements;
            attr = CPS.gfxram[(CPS.scroll3 + 2 * memindex + 1) * 2] * 0x100 + CPS.gfxram[(CPS.scroll3 + 2 * memindex + 1) * 2 + 1];
            if (match == 0)
            {
                Array.Copy(Tilemap.bb0F, 0, pen_data, 0, 0x400);
            }
            else
            {
                Array.Copy(CPS.gfx1rom, code * 0x400, pen_data, 0, 0x400);
            }
            palette_base2 = 0x10 * ((attr & 0x1f) + 0x60);
            flags2 = (byte)(((attr & 0x60) >> 5) & 3);
            group2 = (byte)((attr & 0x0180) >> 7);
            tileflags[logindex] = tile_draw(pen_data, 0, x0, y0, palette_base2, 0, group2, flags2);
        }
        public void tilemap_draw_instance_cps(RECT cliprect, int xpos, int ypos)
        {
            int mincol, maxcol, logindex;
            int x1, y1, x2, y2;
            int y, nexty;
            int offsety1, offsety2;
            int i;
            x1 = Math.Max(xpos, cliprect.min_x);
            x2 = Math.Min(xpos + width, cliprect.max_x + 1);
            y1 = Math.Max(ypos, cliprect.min_y);
            y2 = Math.Min(ypos + height, cliprect.max_y + 1);
            if (x1 >= x2 || y1 >= y2)
                return;
            x1 -= xpos;
            y1 -= ypos;
            x2 -= xpos;
            y2 -= ypos;
            offsety1 = y1;
            mincol = x1 / tilewidth;
            maxcol = (x2 + tilewidth - 1) / tilewidth;
            y = y1;
            nexty = tileheight * (y1 / tileheight) + tileheight;
            nexty = Math.Min(nexty, y2);
            for (; ; )
            {
                int row = y / tileheight;
                trans_t prev_trans = trans_t.WHOLLY_TRANSPARENT;
                trans_t cur_trans;
                int x_start = x1;
                int column;
                for (column = mincol; column <= maxcol; column++)
                {
                    int x_end;
                    if (column == maxcol)
                    {
                        cur_trans = trans_t.WHOLLY_TRANSPARENT;
                    }
                    else
                    {
                        logindex = row * cols + column;
                        if (tileflags[logindex] == Tilemap.TILE_FLAG_DIRTY)
                        {
                            tile_update3(logindex, column, row);
                        }
                        if ((tileflags[logindex] & mask) != 0)
                        {
                            cur_trans = trans_t.MASKED;
                        }
                        else
                        {
                            cur_trans = ((flagsmap[offsety1, column * tilewidth] & mask) == value) ? trans_t.WHOLLY_OPAQUE : trans_t.WHOLLY_TRANSPARENT;
                        }
                    }
                    if (cur_trans == prev_trans)
                    {
                        continue;
                    }
                    x_end = column * tilewidth;
                    x_end = Math.Max(x_end, x1);
                    x_end = Math.Min(x_end, x2);
                    if (prev_trans != trans_t.WHOLLY_TRANSPARENT)
                    {
                        int cury;
                        offsety2 = offsety1;
                        if (prev_trans == trans_t.WHOLLY_OPAQUE)
                        {
                            for (cury = y; cury < nexty; cury++)
                            {
                                Array.Copy(pixmap, offsety2 * width + x_start, Video.bitmapbase[Video.curbitmap], (offsety2 + ypos) * Video.fullwidth + xpos + x_start, x_end - x_start);
                                if (priority != 0)
                                {
                                    for (i = xpos + x_start; i < xpos + x_end; i++)
                                    {
                                        Tilemap.priority_bitmap[offsety2 + ypos, i] = (byte)(Tilemap.priority_bitmap[offsety2 + ypos, i] | priority);
                                    }
                                }
                                offsety2++;
                            }
                        }
                        else if (prev_trans == trans_t.MASKED)
                        {
                            for (cury = y; cury < nexty; cury++)
                            {
                                for (i = xpos + x_start; i < xpos + x_end; i++)
                                {
                                    if ((flagsmap[offsety2, i - xpos] & mask) == value)
                                    {
                                        Video.bitmapbase[Video.curbitmap][(offsety2 + ypos) * Video.fullwidth + i] = pixmap[offsety2 * width + i - xpos];
                                        Tilemap.priority_bitmap[offsety2 + ypos, i] = (byte)(Tilemap.priority_bitmap[offsety2 + ypos, i] | priority);
                                    }
                                }
                                offsety2++;
                            }
                        }
                    }
                    x_start = x_end;
                    prev_trans = cur_trans;
                }
                if (nexty == y2)
                    break;
                offsety1 += (nexty - y);
                y = nexty;
                nexty += tileheight;
                nexty = Math.Min(nexty, y2);
            }
        }
    }
}
