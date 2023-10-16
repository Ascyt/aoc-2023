import re
import json
from Tools import *
print_title('EXTRACT EXAMPLES')

html:str = read('./files/task.html')

print('Finding matches..')
matches = re.findall('<pre><code>(.*?)</code></pre>', html, re.DOTALL)

print('Converting to JSON...')
output:str = json.dumps(matches)

write('./files/examples.json', output)