#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

total=0

for lang in "cs" # specify the language suffixes
do
  count=$(find "$root" -name "*.$lang" | xargs wc -l | tail -n 1 | grep -Po '\d+')
  echo "$lang\t$count lines"
  total=$((total+count))
done

echo ""
echo "total\t$total lines"
