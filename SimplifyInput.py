import SensitiveData
import Config
import openai
from Tools import *

print_title('SIMPLIFY INPUT')

content:str = read('./files/task.html')

print('Generating response...')
openai.api_key = SensitiveData.OPENAI_KEY

response = openai.ChatCompletion.create(
  model=Config.MODEL,
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
  max_tokens=Config.MAXIMUM_LENGTH,
  top_p=1,
  frequency_penalty=0,
  presence_penalty=0
)

write('./files/simplified-task.html', response['choices'][0]['message']['content'])

print('Program finished')