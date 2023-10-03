def write(file_path:str, text:str):
    print(f'Writing "{file_path}"...')
    file = open(file_path, 'w')
    file.write(text)
    file.close()
    print(f'Wrote "{file_path}"')

def print_title(text):
    line:str = '-' * (len(text) + 2)

    print(line)
    print(' ' + text + ' ')
    print(line)
    print()