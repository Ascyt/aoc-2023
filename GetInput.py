import requests
import re

cookies = dict(session=input('Session cookie: '))

def get_html(url)->str:
    try:
        response = requests.get(url, cookies=cookies)
        response.raise_for_status()
        return response.text
    except requests.HTTPError as ex:
        print(f"HTTP error occurred: {ex}")
    except requests.RequestException as ex:
        print(f"Error occurred: {ex}")

def write(file_path, text):
    print(f'Writing "{file_path}"...')
    file = open(file_path, 'w')
    file.write(text)
    file.close()

year:int = input('Year: ')
day:int = input('Day: ')

print()

print('Getting HTML...')
html:str = get_html(f'https://adventofcode.com/{year}/day/{day}')

print('Extracting article...')
match = re.search('<article class="day-desc">(.*?)</article>', html, re.DOTALL)

if match:
    html = match.group(1)

write('./files/task.html', html)

print('Getting input...')
input:str = get_html(f'https://adventofcode.com/{year}/day/{day}/input')

write('./files/input.txt', input)

print('Program finished')