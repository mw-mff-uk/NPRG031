#!/usr/bin/env python3

import argparse
import os
import pathlib

parser = argparse.ArgumentParser(description='Creates a new C# project')
parser.add_argument('project', type=str, help='Name of the project')
args = parser.parse_args()

# Abspath to this directory
DIR = os.path.dirname(os.path.abspath(__file__))

classes_dir = os.path.join(DIR, '.classes')
project_dir = os.path.join(os.path.join(DIR, 'projects'), args.project)

# Check if project directory exists
if (os.path.isdir(project_dir)):
  print("Error: project {} already exists".format(args.project))
  exit(1)

# Create project directory
os.mkdir(project_dir)

# Read the classes
cs_classes = []
for class_file in os.listdir(classes_dir):
  with open(os.path.join(classes_dir, class_file), 'r') as f:
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
with open(os.path.join(project_dir, args.project + '.cs'), 'w') as f:
  f.write(template)

print('Project {} created successfully'.format(args.project))