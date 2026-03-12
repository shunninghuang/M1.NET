using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Drawgfx
    {
        public static void common_drawgfx_namcos1(int sizex, int sizey, int tx, int ty, int code, int color, int flipx, int flipy, int sx, int sy, uint pri_mask, RECT clip)
        {
            int ox;
            int oy;
            int ex;
            int ey;
            ox = sx;
            oy = sy;
            ex = sx + sizex - 1;
            if (sx < 0)
            {
                sx = 0;
            }
            if (sx < clip.min_x)
            {
                sx = clip.min_x;
            }
            if (ex >= 0x200)
            {
                ex = 0x200 - 1;
            }
            if (ex > clip.max_x)
            {
                ex = clip.max_x;
            }
            if (sx > ex)
            {
                return;
            }
            ey = sy + sizey - 1;
            if (sy < 0)
            {
                sy = 0;
            }
            if (sy < clip.min_y)
            {
                sy = clip.min_y;
            }
            if (ey >= 0x200)
            {
                ey = 0x200 - 1;
            }
            if (ey > clip.max_y)
            {
                ey = clip.max_y;
            }
            if (sy > ey)
            {
                return;
            }
            int sw = sizex;
            int sh = sizey;
            int ls = sx - ox;
            int ts = sy - oy;
            int dw = ex - sx + 1;
            int dh = ey - sy + 1;
            int colorbase = 0x10 * color;
            if (color != 0x7f)
            {
                blockmove_8toN_transpen_pri16_namcos1(tx, ty, code, sw, sh, ls, ts, flipx, flipy, dw, dh, colorbase, pri_mask, sx, sy);
            }
            else
            {
                blockmove_8toN_pen_table_pri16_namcos1(tx, ty, code, sw, sh, ls, ts, flipx, flipy, dw, dh, colorbase, pri_mask, sx, sy);
            }
        }
        public static void blockmove_8toN_transpen_pri16_namcos1(int tx, int ty, int code, int srcwidth, int srcheight, int leftskip, int topskip, int flipx, int flipy, int dstwidth, int dstheight, int colorbase, uint pmask, int sx, int sy)
        {
            int xdir, ydir, col, i, j, offsetx, offsety;
            int srcdata_offset = code * 0x400 + tx + ty * 0x20;
            offsetx = sx;
            offsety = sy;
            if (flipy != 0)
            {
                offsety += dstheight - 1;
                srcdata_offset += (srcheight - dstheight - topskip) * 0x20;
                ydir = -1;
            }
            else
            {
                srcdata_offset += topskip * 0x20;
                ydir = 1;
            }
            if (flipx != 0)
            {
                offsetx += dstwidth - 1;
                srcdata_offset += (srcwidth - dstwidth - leftskip);
                xdir = -1;
            }
            else
            {
                srcdata_offset += leftskip;
                xdir = 1;
            }
            for (i = 0; i < dstheight; i++)
            {
                for (j = 0; j < dstwidth; j++)
                {
                    col = Namcos1.gfx3rom[srcdata_offset + i * 0x20 + j];
                    if (col != 0x0f)
                    {
                        if (((1 << (Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x1f)) & pmask) == 0)
                        {
                            if ((Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x80) != 0)
                            {
                                Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = (ushort)shadow_table[0][colorbase + col];
                            }
                            else
                            {
                                Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = (ushort)(colorbase + col);
                            }
                        }
                        Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] = (byte)((Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x7f) | 0x1f);
                    }
                }
            }
        }
        public static void blockmove_8toN_pen_table_pri16_namcos1(int tx, int ty, int code, int srcwidth, int srcheight, int leftskip, int topskip, int flipx, int flipy, int dstwidth, int dstheight, int colorbase, uint pmask, int sx, int sy)
        {
            int xdir, ydir, col, i, j, offsetx, offsety;
            int src_offset;
            int eax = 0x80;
            src_offset = code * 0x400 + tx + ty * 0x20;
            offsetx = sx;
            offsety = sy;
            if (flipy != 0)
            {
                offsety += dstheight - 1;
                src_offset += (srcheight - dstheight - topskip) * 0x20;
                ydir = -1;
            }
            else
            {
                src_offset += topskip * 0x20;
                ydir = 1;
            }
            if (flipx != 0)
            {
                offsetx += dstwidth - 1;
                src_offset += (srcwidth - dstwidth - leftskip);
                xdir = -1;
            }
            else
            {
                src_offset += leftskip;
                xdir = 1;
            }
            for (i = 0; i < dstheight; i++)
            {
                for (j = 0; j < dstwidth; j++)
                {
                    col = Namcos1.gfx3rom[src_offset + i * 0x20 + j];
                    if (col != 0x0f)
                    {
                        switch (gfx_drawmode_table[col])
                        {
                            case 2:
                                afterdrawmask = eax;
                                if (((1 << (Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x1f)) & pmask) == 0)
                                {
                                    if ((Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x80) != 0)
                                    {
                                        Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = (ushort)shadow_table[0][shadow_table[0][Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j]]];
                                    }
                                    else
                                    {
                                        Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j] = (ushort)shadow_table[0][Video.bitmapbase[Video.curbitmap][(offsety + ydir * i) * 0x200 + offsetx + xdir * j]];
                                    }
                                }
                                Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] = (byte)((Tilemap.priority_bitmap[offsety + ydir * i, offsetx + xdir * j] & 0x7f) | afterdrawmask);
                                afterdrawmask = 31;
                                break;
                        }
                    }
                }
            }
        }
    }
}
