import os

def write(file_path:str, text:str):
    print(f'Writing "{file_path}"...')
    os.makedirs(os.path.dirname(file_path), exist_ok=True)
    file = open(file_path, 'w')
    file.write(text)
    file.close()
    print(f'Wrote "{file_path}"')

def read(file_path:str)->str:
    print('Reading file...')
    with open(file_path, 'r') as file:
        return file.read()

def print_title(text):
    line:str = '-' * (len(text) + 2)

    print(line)
    print(' ' + text + ' ')
    print(line)
    print()