#!/usr/bin/env python3

import os
import pathlib
from PyInquirer import prompt

project_name = prompt([{
  'type': 'input',
  'name': 'project_name',
  'message': 'Please, name the project'
}])['project_name']

# Abspath to this directory
DIR = os.path.dirname(os.path.abspath(__file__))

classes_dir = os.path.join(DIR, '.classes')
project_dir = os.path.join(os.path.join(DIR, 'projects'), project_name)

# Check if project directory exists
if (os.path.isdir(project_dir)):
  remove_dir = prompt([{
    'type': 'confirm',
    'name': 'remove_dir',
    'message': 'Project {} already exists. Do you want to rewrite it?'.format(project_name)
  }])['remove_dir']

  if (not remove_dir):
    print("Aborting")
    exit(0)

  remove_dir_confirm = prompt([{
    'type': 'confirm',
    'name': 'remove_dir_confirm',
    'message': 'Are you absolutely sure?'
  }])['remove_dir_confirm']

  if (not remove_dir_confirm):
    print("Aborting")
    exit(0)

  os.rmdir(project_dir)

# Create project directory
os.mkdir(project_dir)

try:
  # Prompt the user to select classes to use
  choices = []
  for class_file in os.listdir(classes_dir):
    choices.append({ 'value': class_file, 'name': class_file })

  class_files = prompt([{
    'type': 'checkbox',
    'name': 'class_files',
    'message': 'Please select classes to use',
    'choices': choices
  }])['class_files']

  # Read the classes
  cs_classes = []
  for class_file in class_files:
    with open(os.path.join(classes_dir, "{}/{}".format(class_file, class_file)), 'r') as f:
      cs_classes.append(f.read())
  cs_classes = '\n'.join(cs_classes)

  # Read the template
  template = None
  with open(os.path.join(DIR, '.templates/project.cs'), 'r') as f:
    template = f.read()

  # Replace mustache placeholders in the template
  template = template.replace('// {{ using }}', '')
  template = template.replace('// {{ main }}', 'InputReader reader = new InputReader();')
  template = template.replace('// {{ classes }}', cs_classes)

  # Create the C# file
  with open(os.path.join(project_dir, '{}.cs'.format(project_name)), 'w') as f:
    f.write(template)

  print('Project {} created successfully'.format(project_name))

except Exception as err:
  os.rmdir(project_dir)
  raise(err)