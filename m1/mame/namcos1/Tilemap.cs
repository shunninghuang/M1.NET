using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Namcos1
    {
        public static Tmap[] ttmap;        
    }
    public partial class Tmap
    {
        public void tile_update_namcos1(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int code, memindex;
            byte flags;
            memindex = logical_to_memory[logindex];
            code = Namcos1.namcos1_videoram[videoram_offset + memindex * 2 + 1] + ((Namcos1.namcos1_videoram[videoram_offset + memindex * 2] & 0x3f) << 8);
            flags = (byte)(attributes & 0x03);
            tileflags[logindex] = tile_draw(Namcos1.gfx2rom, code * 0x40, x0, y0, 0x800, 0, 0, flags);
            tileflags[logindex] = tile_apply_bitmask(Namcos1.gfx1rom, code << 3, x0, y0, 0, flags);
        }
        public void tilemap_draw_instance_namcos1(RECT cliprect, int xpos, int ypos)
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
                        cur_trans = trans_t.WHOLLY_TRANSPARENT;
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
                        continue;
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
                                for (i = xpos + x_start; i < xpos + x_end; i++)
                                {
                                    Video.bitmapbase[Video.curbitmap][(offsety2 + ypos) * 0x200 + i] = (ushort)(pixmap[offsety2 * width + i - xpos] + palette_offset);
                                    Tilemap.priority_bitmap[offsety2 + ypos, i] = (byte)(Tilemap.priority_bitmap[offsety2 + ypos, i] | priority);
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
                                        Video.bitmapbase[Video.curbitmap][(offsety2 + ypos) * 0x200 + i] = (ushort)(pixmap[offsety2 * width + i - xpos] + palette_offset);
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
