#!/bin/sh

root="$( cd "$(dirname "$0")" > /dev/null 2>&1 ; pwd -P )"

program="examples"

mcs "$root/$program.cs" -out:"$root/$program.exe"
mono "$root/$program.exe" --dev
rm "$root/$program.exe"
