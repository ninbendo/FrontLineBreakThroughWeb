# AI Evaluation Result - Codex

## 目的
AI 実装支援（Copilot / Cursor / Claude Code / Codex など）の評価結果を、あとで比較できる形で残す。

## 今回の評価対象
- AI: Codex
- ブランチ: ai-agent-setup-codex

## 評価課題
- テーマ: ③ゲート値更新
- 狙い: 純ロジック切り出しと Edit Mode テスト化の適性を見る
- 実装対象候補:
  - GateController
  - 新規 Rules / Calculator クラス
  - Edit Mode テスト

## 参照資料
- docs/specs/1.ゲーム要件.txt
- docs/specs/2.ゲーム設計書.txt
- docs/specs/3.技術設計メモ（クラス責務・参照関係）.txt
- docs/specs/4.実装タスク票.txt
- docs/specs/5.受け入れテスト表.txt
- docs/specs/6.自動テスト方針メモ（Unity Test Framework導入用）.txt
- AGENTS.md
- docs/ai-eval-runbook.md

## 比較観点
- 読み取り精度
- 指示遵守
- 小変更の安全性
- Unity文脈との整合
- 不要変更の少なさ
- テストの妥当性
- 修正往復回数
- 導入しやすさ
- コスト感

## 実施結果

- 実施日: 2026-03-12
- 実施AI: Codex
- 実施ブランチ: ai-agent-setup-codex
- 結果: Partial
- 点数目安: 7.0 / 10

### 実施内容
- Codex 拡張のセットアップ確認
- Unity MCP の評価用セットアップ
- ③ゲート値更新の評価開発
  - 確認
  - 計画
  - 最小差分実装
  - 差分レビュー
- Unity MCP を用いた Editor 状態読解評価
  - MCP サーバー接続確認
  - Unity Session 接続確認
  - Active Scene / Console / GameObject 読み取り確認
  - Gate 関連探索の再指示評価

### 変更ファイル（評価開発で触れたもの）
- .gitignore
- .vscode/mcp.json
- Packages/manifest.json
- Packages/packages-lock.json
- Assets/Scripts/GateValueCalculator.cs
- Assets/Scripts/GateController.cs
- Assets/Scripts/Combat/BulletController.cs
- Assets/Scripts/PlayerGroupController.cs
- Assets/Tests/EditMode/GateValueCalculatorTests.cs

### 良かった点
- 段階的な指示（確認 → 計画 → 変更 → レビュー）への追従が良かった
- 最小差分志向が比較的強く、Copilot より不要変更が少なかった
- 仕様「1発で1変化」「負→0→正」「接触時に一度だけ反映」に合わせて、人間レビュー後の修正追従ができた
- GateValueCalculator のような純ロジック切り出しは相性が良かった
- Unity MCP の切り分けで、
  - MCP サーバー接続
  - Unity Session 未接続
  - Session Active 後の再確認
  を整理できた
- read-only 条件下でも、再指示後は探索を広げ、
  - PlayerGroupController
  - PlayerShooter
  - Bullet.prefab
  - BulletController
  - Spike / Barrel の Collider / isTrigger
  まで辿れた
- 「断定できること」と「推測」を分けて返せた

### 問題点
- 初回は変更系ツール（Manage Scene / Manage Asset）を使おうとした
- read-only 条件を最初から完全には守れず、人間側で Deny 制御が必要だった
- Unity MCP の Start Server は、この Windows 環境では uvx.exe が Device Guard にブロックされ、そのままでは起動できなかった
- `uv.exe tool run` による手動起動という人間の補助が必要だった
- Gate の直接特定は弱く、単純検索では 0 件で止まりやすかった
- Scene / Prefab / Script 参照の横断探索は、再指示しないと浅くなりやすかった
- テストまで AI に任せても、Unity 上の最終確認や Inspector 前提の確認は人間が必要だった

### 判定メモ
- Copilot より、読み取り精度・差分の安全性・再指示後の探索の深さは良かった
- 特に Unity MCP で「接続できているか」「何が未接続か」を切り分ける能力は有用だった
- 一方で、完全自律で Unity 実装を完結させるにはまだ不足がある
- 今回は「有力な補助AI」ではあるが、「主担当AI」として全面委任するには不安が残る
- 実用上は、
  - 読み取り
  - 計画
  - 最小差分実装
  - 差分レビュー補助
  に向く
- Play確認、Inspector設定確認、最終受け入れ判断は人間前提が必要
- 9観点で見ると、読み取り精度・指示遵守・小変更の安全性・導入しやすさは比較的良好だった一方、完全自律性と最終品質保証はまだ人間前提だった

### 次回改善点
- 変更系ツールを使わせない評価では、最初のプロンプトで read-only 制約をさらに明示する
- 「Gate 文字列検索」だけでなく、探索方法を指定して比較する
- Unity MCP の起動方式は、Windows 環境では `uvx.exe` ではなく `uv.exe tool run` を評価手順に明記する
- 次回も同一課題「③ゲート値更新」で比較し、Prompt の固定度をさらに上げる
- Play / Edit Mode テスト結果まで含める場合は、AI の自律性と人間介入点を分けて記録する

### 最終判断
- 採用したもの:
  - Unity MCP 最小セットアップ
  - ③ゲート値更新の最小差分実装方針
  - GateValueCalculator 切り出し
  - Edit Mode テスト追加の方向性
- 採用しなかったもの:
  - 変更系ツールを使う前提の探索
  - AI単独での最終動作保証
- 理由:
  - Codex は補助としてかなり有効だった
  - ただし、Unity の Scene / Inspector / Trigger / Play 結果を含む最終品質は人間レビューが必要
  - 今回は Partial が妥当
