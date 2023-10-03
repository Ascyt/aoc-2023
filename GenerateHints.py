import OpenaiKey
import openai
from Tools import *

write_title('GENERATE HINTS')

model:str = input('Use GPT-4? (y/N): ')
model = 'gpt-4' if len(model) > 0 and model[0].lower() == 'y' else 'gpt-3.5-turbo-16k'

print('Reading file...')
with open('./files/simplified-task.html', 'r') as file:
    content:str = file.read()

print('Generating response...')
openai.api_key = OpenaiKey.OPENAI_KEY

response = openai.ChatCompletion.create(
  model=model,
  messages=[
    {
      "role": "system",
      "content": "You will be provided with a task without its actual input. You need to respond with hints, guides, and a possible way of solving the problem. Include everything that might be helpful. Respond in Markdown."
    },
    {
      "role": "user",
      "content": content
    }
  ],
  temperature=1,
  max_tokens=8192,
  top_p=1,
  frequency_penalty=0,
  presence_penalty=0
)

write('./files/hints.md', response['choices'][0]['message']['content'])

print('Program finished')