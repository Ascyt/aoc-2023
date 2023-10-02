import OpenaiKey
import openai
import GetInput

print('Reading file...')
with open('./files/task.html', 'r') as file:
    content:str = file.read()

print('Generating response...')
openai.api_key = OpenaiKey.OPENAI_KEY

response = openai.ChatCompletion.create(
  model="gpt-3.5-turbo-16k",
  messages=[
    {
      "role": "system",
      "content": "You will be provided with a task without its actual input. You need to respond with a simplified and prettier version of this task in an HTML format. Cut out any information that's not important for the actual task, however make sure to leave in any examples or hints that could be helpful to completing the task. Leave anything that could be helpful in, and feel free to add some tips yourself."
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

GetInput.write('./files/simplified-task.html', response['choices'][0]['message']['content'])

print('Program finished')