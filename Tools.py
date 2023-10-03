def write(file_path:str, text:str):
    print(f'Writing "{file_path}"...')
    file = open(file_path, 'w')
    file.write(text)
    file.close()
    print(f'Wrote "{file_path}"')
