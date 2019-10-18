# osu!lazer beatmap exporter
A tool, that exports all of your osu!lazer beatmaps back to .osz files, or folders.

I noticed some people on twitter and in osu!lazer issues wanted to export their beatmaps, so i decided to give it a try and wrote this simple program.

### Usage
Launch OsuLazerBeatmapExporter in command line.  
``OsuLazerBeatmapExporter [options]``

Available options:

| Option name             | Default value | Description                                                  |
|-------------------------|---------------|--------------------------------------------------------------|
| -e -t -type -exportType | Folder           | Export type of beatmaps. (Should be either osz or folder)    |
| -l -lazer -lazerPath    | %appdata%\osu | osu!lazer directory. (On windows it's usually %appdata%\osu) |
| -o -output -outputPath  | Beatmaps      | The directory where beatmaps will be exported.               |

Default values will be used if some or all of these options are not specified.  
You can also just run an executable if you don't want to use any of these options.
