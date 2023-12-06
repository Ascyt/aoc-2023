import requests
import re
from Tools import *
from datetime import datetime
import time
import sys
import webbrowser
import subprocess

cookies = {}

def get_html(url):
    response = requests.get(url, cookies=cookies)
    return response

print_title('GET INPUT')

cookies = dict({'session':input('Session cookie: ')})

now = datetime.now()

day:int = now.day
year:int = now.year


print(f'Year {year} | Day {day}')

print()

print('Getting HTML...')

retries:int = 0
html:str = None

def clear_line():
    sys.stdout.write("\033[F")  # Cursor up one line
    sys.stdout.write("\033[K")  # Clear to the end of line

link:str = f'https://adventofcode.com/{year}/day/{day}'

while True:
    response:str = get_html(link)
    if response.status_code == 200:
        print('Successfully fetched HTML')
        html = response.text
        break

    if response.status_code == 404:
        retries += 1
        clear_line()
        print(f'Not found. (#{retries})')
        time.sleep(1)
        clear_line()
        print(f'Not found. (#{retries}) Trying again ...')
        continue

    raise requests.HTTPError(response=response)

webbrowser.open(link)

print('Extracting article...')
match = re.search('<article class="day-desc">(.*?)</article>', html, re.DOTALL)

if match:
    html = match.group(1)

write('./files/task.html', html)

print('Getting input...')
input:str = get_html(f'https://adventofcode.com/{year}/day/{day}/input').text

write('./files/input.txt', input)

print('Opening in notepad.exe...')

def open_file_in_notepad(file_path):
    try:
        # Try to open the file in an existing Notepad instance
        subprocess.Popen(['notepad.exe', file_path])
    except FileNotFoundError:
        # If Notepad is not currently open, open the file in a new Notepad instance
        subprocess.Popen(['start', 'notepad.exe', file_path])

open_file_in_notepad('./files/input.txt')
