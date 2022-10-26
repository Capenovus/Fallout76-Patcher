import os, sys, zlib

def get_crc32(raf):
	raf.seek(0)
	crc = 0
	while True:
		buffer = raf.read(128 * 1024)
		if len(buffer) == 0:
			return reverse32(crc)
		crc = zlib.crc32(buffer, crc)

def reverse32(x):
	y = 0
	for i in range(32):
		y = (y << 1) | (x & 1)
		x >>= 1
	return y


def main(args):
    if len(args) != 1:
        return "Usage: python get_crc.py FileName"
    try:
        with open(args[0], "r+b") as raf:
            raf.seek(0, os.SEEK_END)
            length = raf.tell()
            if 12 + 4 > length:
                raise ValueError("Byte offset plus 4 exceeds file length")
            
            # Read entire file and calculate original CRC-32 value
            crc = get_crc32(raf)
            print(f"{reverse32(crc):08X}")
    except Exception as e: print(e)
        
if __name__ == "__main__":
	errmsg = main(sys.argv[1:])
	if errmsg is not None:
		sys.exit(errmsg)
