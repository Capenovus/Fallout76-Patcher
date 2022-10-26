:fcrc32

:: stops writing commands in script
@echo off

:: CRC Value
set crc=799A1939

:: executes fcrc32.py
python fcrc32.py SeventySix.esm 12 %crc% || py fcrc32.py SeventySix.esm 12 %crc% || python3 fcrc32.py SeventySix.esm 12 %crc%