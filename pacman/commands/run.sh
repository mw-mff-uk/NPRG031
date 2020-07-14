#!/bin/sh

dir="$( cd "$(dirname "$0")" > /dev/null 2>&1 ; pwd -P )"
root="$dir/.."

mono "$root/pacman.exe"
