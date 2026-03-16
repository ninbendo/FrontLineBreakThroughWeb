#!/bin/bash
# PreToolUse hook: Edit/Write対象が変更禁止ファイルならブロックする
# stdin から JSON を受け取り、file_path をチェックする

input=$(cat)
file_path=$(echo "$input" | grep -o '"file_path"[[:space:]]*:[[:space:]]*"[^"]*"' | head -1 | sed 's/.*"file_path"[[:space:]]*:[[:space:]]*"//' | sed 's/"$//')

if [ -z "$file_path" ]; then
  exit 0
fi

# 変更禁止ファイルリスト
BLOCKED_FILES=(
  "ProjectSettings/ProjectSettings.asset"
  "Packages/manifest.json"
)

for blocked in "${BLOCKED_FILES[@]}"; do
  if echo "$file_path" | grep -q "$blocked"; then
    echo "BLOCKED: $file_path は変更禁止ファイルです（AGENTS.md参照）"
    exit 2
  fi
done

exit 0
