using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mame
{
    public partial class Technos
    {
        public static void SaveStateBinary_ddragon(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw0);
            writer.Write(dsw1);
            writer.Write(ddragon_scrollx_hi);
            writer.Write(ddragon_scrolly_hi);
            writer.Write(ddragon_scrollx_lo);
            writer.Write(ddragon_scrolly_lo);
            writer.Write(technos_video_hw);
            writer.Write(dd_sub_cpu_busy);
            writer.Write(sprite_irq);
            writer.Write(sound_irq);
            writer.Write(ym_irq);
            writer.Write(snd_cpu);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_pos[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_end[i]);
            }
            writer.Write(adpcm_idle, 0, 2);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_data[i]);
            }
            writer.Write(scanline_param);
            writer.Write(basebankmain);
            for (i = 0; i < 0x180; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x1000);
            writer.Write(mainram2, 0, 0x400);
            writer.Write(subram, 0, 0x1000);
            writer.Write(Memory.audioram, 0, 0x1000);
            writer.Write(ddragon_bgvideoram, 0, 0x800);
            writer.Write(ddragon_fgvideoram, 0, 0x800);
            writer.Write(ddragon_spriteram, 0, 0x1000);
            writer.Write(Generic.paletteram, 0, 0x200);
            writer.Write(Generic.paletteram_2, 0, 0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].SaveStateBinary(writer);
            }
            Cpuexec.SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);            
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            for (i = 0; i < 2; i++)
            {
                MSM5205.mm1[i].SaveStateBinary(writer);
            }
            writer.Write(Sound.latched_value[0]);
            writer.Write(Sound.utempdata[0]);
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            for (i = 0; i < 2; i++)
            {
                writer.Write(MSM5205.mm1[i].voice.stream.output_sampindex);
                writer.Write(MSM5205.mm1[i].voice.stream.output_base_sampindex);
            }
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_ddragon(BinaryReader reader)
        {
            int i;
            dsw0 = reader.ReadByte();
            dsw1 = reader.ReadByte();
            ddragon_scrollx_hi = reader.ReadUInt16();
            ddragon_scrolly_hi = reader.ReadUInt16();
            ddragon_scrollx_lo = reader.ReadByte();
            ddragon_scrolly_lo = reader.ReadByte();
            technos_video_hw = reader.ReadByte();
            dd_sub_cpu_busy = reader.ReadByte();
            sprite_irq = reader.ReadByte();
            sound_irq = reader.ReadByte();
            ym_irq = reader.ReadByte();
            snd_cpu = reader.ReadByte();
            for (i = 0; i < 2; i++)
            {
                adpcm_pos[i] = reader.ReadUInt32();
            }
            for (i = 0; i < 2; i++)
            {
                adpcm_end[i] = reader.ReadUInt32();
            }
            adpcm_idle = reader.ReadBytes(2);
            for (i = 0; i < 2; i++)
            {
                adpcm_data[i] = reader.ReadInt32();
            }            
            scanline_param = reader.ReadInt32();
            basebankmain = reader.ReadInt32();
            for (i = 0; i < 0x180; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x1000);
            mainram2 = reader.ReadBytes(0x400);
            subram = reader.ReadBytes(0x1000);
            Memory.audioram = reader.ReadBytes(0x1000);
            ddragon_bgvideoram = reader.ReadBytes(0x800);
            ddragon_fgvideoram = reader.ReadBytes(0x800);
            ddragon_spriteram = reader.ReadBytes(0x1000);
            Generic.paletteram = reader.ReadBytes(0x200);
            Generic.paletteram_2 = reader.ReadBytes(0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].LoadStateBinary(reader);
            }
            Cpuexec.LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();            
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            for (i = 0; i < 2; i++)
            {
                MSM5205.mm1[i].LoadStateBinary(reader);
            }
            Sound.latched_value[0] = reader.ReadUInt16();
            Sound.utempdata[0] = reader.ReadUInt16();
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                MSM5205.mm1[i].voice.stream.output_sampindex = reader.ReadInt32();
                MSM5205.mm1[i].voice.stream.output_base_sampindex = reader.ReadInt32();
            }
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_ddragon2(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw0);
            writer.Write(dsw1);
            writer.Write(ddragon_scrollx_hi);
            writer.Write(ddragon_scrolly_hi);
            writer.Write(ddragon_scrollx_lo);
            writer.Write(ddragon_scrolly_lo);
            writer.Write(technos_video_hw);
            writer.Write(dd_sub_cpu_busy);
            writer.Write(sprite_irq);
            writer.Write(sound_irq);
            writer.Write(ym_irq);
            writer.Write(snd_cpu);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_data[i]);
            }
            writer.Write(scanline_param);
            writer.Write(basebankmain);
            for (i = 0; i < 0x180; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x1800);
            writer.Write(subram, 0, 0x1000);
            writer.Write(Memory.audioram, 0, 0x800);
            writer.Write(ddragon_bgvideoram, 0, 0x800);
            writer.Write(ddragon_fgvideoram, 0, 0x800);
            writer.Write(ddragon_spriteram, 0, 0x1000);
            writer.Write(Generic.paletteram, 0, 0x200);
            writer.Write(Generic.paletteram_2, 0, 0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].SaveStateBinary(writer);
            }
            Cpuexec.SaveStateBinary(writer);            
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            writer.Write(Sound.latched_value[0]);
            writer.Write(Sound.utempdata[0]);
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_ddragon2(BinaryReader reader)
        {
            int i;
            dsw0 = reader.ReadByte();
            dsw1 = reader.ReadByte();
            ddragon_scrollx_hi = reader.ReadUInt16();
            ddragon_scrolly_hi = reader.ReadUInt16();
            ddragon_scrollx_lo = reader.ReadByte();
            ddragon_scrolly_lo = reader.ReadByte();
            technos_video_hw = reader.ReadByte();
            dd_sub_cpu_busy = reader.ReadByte();
            sprite_irq = reader.ReadByte();
            sound_irq = reader.ReadByte();
            ym_irq = reader.ReadByte();
            snd_cpu = reader.ReadByte();
            scanline_param = reader.ReadInt32();
            basebankmain = reader.ReadInt32();
            for (i = 0; i < 0x180; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x1800);
            subram = reader.ReadBytes(0x1000);
            Memory.audioram = reader.ReadBytes(0x800);
            ddragon_bgvideoram = reader.ReadBytes(0x800);
            ddragon_fgvideoram = reader.ReadBytes(0x800);
            ddragon_spriteram = reader.ReadBytes(0x1000);
            Generic.paletteram = reader.ReadBytes(0x200);
            Generic.paletteram_2 = reader.ReadBytes(0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].LoadStateBinary(reader);
            }
            Cpuexec.LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();            
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            Sound.latched_value[0] = reader.ReadUInt16();
            Sound.utempdata[0] = reader.ReadUInt16();
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_darktowr(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw0);
            writer.Write(dsw1);
            writer.Write(ddragon_scrollx_hi);
            writer.Write(ddragon_scrolly_hi);
            writer.Write(ddragon_scrollx_lo);
            writer.Write(ddragon_scrolly_lo);
            writer.Write(technos_video_hw);
            writer.Write(dd_sub_cpu_busy);
            writer.Write(sprite_irq);
            writer.Write(sound_irq);
            writer.Write(ym_irq);
            writer.Write(snd_cpu);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_pos[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_end[i]);
            }
            writer.Write(adpcm_idle, 0, 2);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_data[i]);
            }
            writer.Write(darktowr_mcu_ports, 0, 8);
            writer.Write(scanline_param);
            writer.Write(basebankmain);
            for (i = 0; i < 0x180; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x1000);
            writer.Write(mainram2, 0, 0x400);
            writer.Write(subram, 0, 0x1000);
            writer.Write(Memory.audioram, 0, 0x1000);
            writer.Write(mcuram, 0, 0x80);
            writer.Write(ddragon_bgvideoram, 0, 0x800);
            writer.Write(ddragon_fgvideoram, 0, 0x800);
            writer.Write(ddragon_spriteram, 0, 0x1000);
            writer.Write(Generic.paletteram, 0, 0x200);
            writer.Write(Generic.paletteram_2, 0, 0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].SaveStateBinary(writer);
            }
            Cpuexec.SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            for (i = 0; i < 2; i++)
            {
                MSM5205.mm1[i].SaveStateBinary(writer);
            }
            writer.Write(Sound.latched_value[0]);
            writer.Write(Sound.utempdata[0]);
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            for (i = 0; i < 2; i++)
            {
                writer.Write(MSM5205.mm1[i].voice.stream.output_sampindex);
                writer.Write(MSM5205.mm1[i].voice.stream.output_base_sampindex);
            }
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_darktowr(BinaryReader reader)
        {
            int i;
            dsw0 = reader.ReadByte();
            dsw1 = reader.ReadByte();
            ddragon_scrollx_hi = reader.ReadUInt16();
            ddragon_scrolly_hi = reader.ReadUInt16();
            ddragon_scrollx_lo = reader.ReadByte();
            ddragon_scrolly_lo = reader.ReadByte();
            technos_video_hw = reader.ReadByte();
            dd_sub_cpu_busy = reader.ReadByte();
            sprite_irq = reader.ReadByte();
            sound_irq = reader.ReadByte();
            ym_irq = reader.ReadByte();
            snd_cpu = reader.ReadByte();
            for (i = 0; i < 2; i++)
            {
                adpcm_pos[i] = reader.ReadUInt32();
            }
            for (i = 0; i < 2; i++)
            {
                adpcm_end[i] = reader.ReadUInt32();
            }
            adpcm_idle = reader.ReadBytes(2);
            for (i = 0; i < 2; i++)
            {
                adpcm_data[i] = reader.ReadInt32();
            }
            darktowr_mcu_ports = reader.ReadBytes(8);
            scanline_param = reader.ReadInt32();
            basebankmain = reader.ReadInt32();
            for (i = 0; i < 0x180; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x1000);
            mainram2 = reader.ReadBytes(0x400);
            subram = reader.ReadBytes(0x1000);
            Memory.audioram = reader.ReadBytes(0x1000);
            mcuram = reader.ReadBytes(0x80);
            ddragon_bgvideoram = reader.ReadBytes(0x800);
            ddragon_fgvideoram = reader.ReadBytes(0x800);
            ddragon_spriteram = reader.ReadBytes(0x1000);
            Generic.paletteram = reader.ReadBytes(0x200);
            Generic.paletteram_2 = reader.ReadBytes(0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].LoadStateBinary(reader);
            }
            Cpuexec.LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            for (i = 0; i < 2; i++)
            {
                MSM5205.mm1[i].LoadStateBinary(reader);
            }
            Sound.latched_value[0] = reader.ReadUInt16();
            Sound.utempdata[0] = reader.ReadUInt16();
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                MSM5205.mm1[i].voice.stream.output_sampindex = reader.ReadInt32();
                MSM5205.mm1[i].voice.stream.output_base_sampindex = reader.ReadInt32();
            }
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_toffy(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw0);
            writer.Write(dsw1);
            writer.Write(ddragon_scrollx_hi);
            writer.Write(ddragon_scrolly_hi);
            writer.Write(ddragon_scrollx_lo);
            writer.Write(ddragon_scrolly_lo);
            writer.Write(technos_video_hw);
            writer.Write(dd_sub_cpu_busy);
            writer.Write(sprite_irq);
            writer.Write(sound_irq);
            writer.Write(ym_irq);
            writer.Write(snd_cpu);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_pos[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_end[i]);
            }
            writer.Write(adpcm_idle, 0, 2);
            for (i = 0; i < 2; i++)
            {
                writer.Write(adpcm_data[i]);
            }
            writer.Write(scanline_param);
            writer.Write(basebankmain);
            for (i = 0; i < 0x180; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x1000);
            writer.Write(mainram2, 0, 0x400);
            writer.Write(Memory.audioram, 0, 0x1000);
            writer.Write(ddragon_bgvideoram, 0, 0x800);
            writer.Write(ddragon_fgvideoram, 0, 0x800);
            writer.Write(ddragon_spriteram, 0, 0x1000);
            writer.Write(Generic.paletteram, 0, 0x200);
            writer.Write(Generic.paletteram_2, 0, 0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].SaveStateBinary(writer);
            }
            Cpuexec.SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            writer.Write(Sound.latched_value[0]);
            writer.Write(Sound.utempdata[0]);
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_toffy(BinaryReader reader)
        {
            int i;
            dsw0 = reader.ReadByte();
            dsw1 = reader.ReadByte();
            ddragon_scrollx_hi = reader.ReadUInt16();
            ddragon_scrolly_hi = reader.ReadUInt16();
            ddragon_scrollx_lo = reader.ReadByte();
            ddragon_scrolly_lo = reader.ReadByte();
            technos_video_hw = reader.ReadByte();
            dd_sub_cpu_busy = reader.ReadByte();
            sprite_irq = reader.ReadByte();
            sound_irq = reader.ReadByte();
            ym_irq = reader.ReadByte();
            snd_cpu = reader.ReadByte();
            for (i = 0; i < 2; i++)
            {
                adpcm_pos[i] = reader.ReadUInt32();
            }
            for (i = 0; i < 2; i++)
            {
                adpcm_end[i] = reader.ReadUInt32();
            }
            adpcm_idle = reader.ReadBytes(2);
            for (i = 0; i < 2; i++)
            {
                adpcm_data[i] = reader.ReadInt32();
            }
            scanline_param = reader.ReadInt32();
            basebankmain = reader.ReadInt32();
            for (i = 0; i < 0x180; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x1000);
            mainram2 = reader.ReadBytes(0x400);
            Memory.audioram = reader.ReadBytes(0x1000);
            ddragon_bgvideoram = reader.ReadBytes(0x800);
            ddragon_fgvideoram = reader.ReadBytes(0x800);
            ddragon_spriteram = reader.ReadBytes(0x1000);
            Generic.paletteram = reader.ReadBytes(0x200);
            Generic.paletteram_2 = reader.ReadBytes(0x200);
            for (i = 0; i < Cpuexec.ncpu; i++)
            {
                Cpuexec.cpu[i].LoadStateBinary(reader);
            }
            Cpuexec.LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            Sound.latched_value[0] = reader.ReadUInt16();
            Sound.utempdata[0] = reader.ReadUInt16();
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}
