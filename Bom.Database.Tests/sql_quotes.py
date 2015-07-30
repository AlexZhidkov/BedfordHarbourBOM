import sys
if len(sys.argv) < 2:
    print("specify the file name")
    exit()
fn = sys.argv[1]
out_fn = fn + "_s"
out_f = open(out_fn, 'w+')
with open(fn) as in_f:
    lines = in_f.readlines()
    sz = len(lines)
    for i, line in enumerate(lines):
        new_line = '\"' + line.strip('\r\n') + '\"'
        if i != sz-1:
            new_line += ' +'
        new_line += '\n'
        out_f.write(new_line)
out_f.close()
