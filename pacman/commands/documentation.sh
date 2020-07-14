#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

mdpdf "$root/readme.md" "$root/documentation.pdf"
