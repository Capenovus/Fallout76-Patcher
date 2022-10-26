@echo off

set filename=SeventySix.esm

python get_crc.py %filename% || py get_crc.py %filename% || python3 get_crc.py %filename%

pause