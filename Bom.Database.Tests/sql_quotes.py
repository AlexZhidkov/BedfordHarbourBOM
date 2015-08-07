# Formats SQL to insert into C# code (for unit tests):
# adds '" +' to the end of each line (except the last)
# to the original SQL statement, read from file

import sys
if len(sys.argv) < 2:
    print("specify the file name")
    exit()
fn = sys.argv[1]
ls = fn.split(".")
out_fn = ls[0] + "_q." + ls[1]

out_f = open(out_fn, 'w+')
with open(fn) as in_f:
    lines = in_f.readlines()
    sz = len(lines)
    new_sql = ""
    for line in lines:
        new_sql += '\"' + line.strip('\r\n') + '\" +\n'
    
    out_f.write(new_sql[:-2])
out_f.close()
