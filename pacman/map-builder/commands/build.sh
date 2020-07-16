#!/bin/sh

dir="$( cd "$(dirname "$0")" > /dev/null 2>&1 ; pwd -P )"
root="$dir/.."

csc \
  "$root/src/*.cs" \
  -out:"$root/map-builder.exe"
