import SensitiveData
import openai
from Tools import *

print_title('SIMPLIFY INPUT')

content:str = read('./files/simplified-task.html')

print('Generating response...')
openai.api_key = SensitiveData.OPENAI_KEY

response = openai.ChatCompletion.create(
  model="gpt-3.5-turbo-16k",
  messages=[
    {
      "role": "system",
      "content": "You will be provided with a task without its actual input. You need to respond with a simplified and prettier version of this task in an HTML format. Cut out any information that's neither important nor helpful for the actual task. Leave anything that could be helpful in, and feel free to add some tips yourself."
    },
    {
      "role": "user",
      "content": content
    }
  ],
  temperature=0.8,
  max_tokens=8192,
  top_p=1,
  frequency_penalty=0,
  presence_penalty=0
)

write('./files/simplified-task.html', response['choices'][0]['message']['content'])

print('Program finished')