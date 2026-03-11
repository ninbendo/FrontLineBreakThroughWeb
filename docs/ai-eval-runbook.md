# AI Eval Runbook

## 目的
AI 実装支援（Copilot / Cursor / Claude Code / Codex など）の評価結果を、あとで比較できる形で残す。

## 今回の評価対象
- AI: GitHub Copilot / Copilot Chat
- ブランチ: ai-agent-setup-copilot

## 評価課題
- テーマ: ③ゲート値更新
- 狙い: 純ロジック切り出しと Edit Mode テスト化の適性を見る
- 実装対象候補:
  - GateController
  - 新規 Rules / Calculator クラス
  - Edit Mode テスト

## 参照資料
- specs/1.ゲーム要件.txt
- specs/2.ゲーム設計書.txt
- specs/3.技術設計メモ（クラス責務・参照関係）.txt
- specs/4.実装タスク票.txt
- specs/5.受け入れテスト表.txt
- specs/6.自動テスト方針メモ（Unity Test Framework導入用）.txt

## 判定基準
- 導入しやすさ
- 読み取り精度
- 小変更の安全性
- 自律性の質
- コスト感

## 実施結果

- 実施日: 2026-03-11
- 実施AI: GitHub Copilot / Copilot Chat
- 実施ブランチ: ai-agent-setup-copilot
- 結果: Partial

### 実施内容
- Unity MCP の最小セットアップ
- AGENTS.md / ai-eval-runbook.md / docs/specs の整備
- ③ゲート値更新の評価開発
  - 読み取り
  - 設計提案
  - 最小差分実装
  - 差分レビュー

### 変更ファイル（評価開発で触れたもの）
- AGENTS.md
- docs/ai-eval-runbook.md
- docs/specs/1.ゲーム要件.txt
- docs/specs/2.ゲーム設計書.txt
- docs/specs/3.技術設計メモ（クラス責務・参照関係）.txt
- docs/specs/4.実装タスク票.txt
- docs/specs/5.受け入れテスト表.txt
- docs/specs/6.自動テスト方針メモ（Unity Test Framework導入用）.txt
- Assets/Scripts/GateValueCalculator.cs
- Assets/Scripts/GateController.cs
- Assets/Scripts/PlayerGroupController.cs
- Assets/Tests/EditMode/GateValueCalculatorTests.cs

### 良かった点
- ワークスペース内検索と資料参照は概ね可能
- 段階的な指示（確認 → 提案 → 変更）には追従できた
- docs/specs 配下への資料整理、参照更新、ルール文書整備には活用しやすかった
- 純ロジック切り出しと Edit Mode テスト追加という方向性自体は提案できた

### 問題点
- Unity 実装の文脈理解が弱く、既存実装との整合確認が浅かった
- 指示しても API を広げたり、不要な汎用化を入れる傾向があった
- 仕様「弾1発で1変化」「負→0→正」に対して、初回実装でズレが出た
- テストと実装の整合が一度崩れ、追加修正が必要になった
- 2D / 3D 物理の前提確認が甘く、OnTriggerEnter 系の実装妥当性に不安が残った
- 今回の最終実装差分は採用せず、破棄判断とした

### 判定メモ
- 資料整理・参照更新・ルールファイル整備のようなテキスト中心作業には有効
- Unity の評価開発では、指示遵守と既存コード整合の精度が十分とは言えない
- 小規模補助には使えるが、今回の評価では主担当 AI とするには不安が残った

### 次回改善点
- 次のAI評価でも同一課題「③ゲート値更新」を使い、同条件で比較する
- 評価手順は「読み取り → 設計提案 → 最小変更 → 差分レビュー」で統一する
- 実装差分は人間レビュー前提を維持する
- Unity 文脈の既存前提（2D/3D、既存責務、接続方式）を先に確認させる

### 次回方針
- ai-agent-setup-copilot ブランチは、セットアップと評価記録を残して一旦停止する
- ③ゲート値更新の実装差分は採用しない
- 次回は main から新しい評価ブランチを切り、別AIで同課題を再評価する
- 次の評価優先度:
  1. Codex
  2. Claude Code
