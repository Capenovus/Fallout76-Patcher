# Fallout76-Patcher

Automatically patch SeventySix.esm with a range of different patches.

# Features

- Automatic hex pattern patching
- Automatic CRC Signature patch

# Requirements
- [Python](https://www.python.org/downloads/)

# Usage
1. Put `SeventySix.esm` in the same folder as the Patcher
2. Ensure that the specified CRC Value in `fcrc32.bat` is up to date using `get_crc.bat`
3. Run the Patcher and select your desired patches
<br> You may select multiple patches at once by using comma separated syntax
4. Run the batch script or alternatively do it manually:
<br> Steam Version: Copy the new `SeventySix.esm` file to `%localappdata%\Fallout76\associated_media\data\SeventySix.esm` 
<br> Windows Store Version: Copy the new `SeventySix.esm` file to `%localappdata%\Project76\associated_media\data\SeventySix.esm`

# Note
I do not ensure the functionality of any patches provided.

# Credits

Byte patterns taken from [Suchi96](https://github.com/Suchi96/Fallout-76-Modding)
<br>
If you have additional scripts to contribute, feel free to do so
