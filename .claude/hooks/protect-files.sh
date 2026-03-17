#!/bin/bash
# PreToolUse hook: Edit/Write対象が変更禁止ファイルならブロックする
# stdin から JSON を受け取り、tool_input.file_path をチェックする

input=$(cat)

# tool_input.file_path を抽出（jq未使用、grep+sedで対応）
file_path=$(echo "$input" | grep -o '"file_path" *: *"[^"]*"' | head -1 | sed 's/.*: *"//;s/"$//')

if [ -z "$file_path" ]; then
  exit 0
fi

# Windowsパス（バックスラッシュ）をスラッシュに正規化
file_path=$(echo "$file_path" | sed 's|\\\\|/|g' | sed 's|\\|/|g')

# 変更禁止ファイルリスト
BLOCKED_FILES=(
  "ProjectSettings/ProjectSettings.asset"
  "Packages/manifest.json"
)

for blocked in "${BLOCKED_FILES[@]}"; do
  if [[ "$file_path" == *"$blocked"* ]]; then
    echo "BLOCKED: $file_path は変更禁止ファイルです（AGENTS.md/CLAUDE.md参照）" >&2
    exit 2
  fi
done

exit 0
