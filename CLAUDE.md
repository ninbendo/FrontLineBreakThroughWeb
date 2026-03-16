# CLAUDE.md

## プロジェクト概要

FrontLineBreakThroughWeb: Last War: Survival Gameの「前線突破」ミニゲームをUnity WebGLで再現するプロジェクト。
本家の完全コピーを目指す。

## 技術スタック

- Unity 6 (6000.3.9f1) / WebGL / 縦画面
- 3D見下ろし斜め視点（3D Collider/Trigger実装。2D専用API禁止）
- TextMeshPro（UI表示）

## ドキュメント参照順

仕様の確認が必要なときは以下の順で参照する:
1. `docs/specs/1.ゲーム要件.txt` - 何を作るか
2. `docs/specs/2.ゲーム設計書.txt` - どう動くか
3. `docs/specs/3.技術設計メモ（クラス責務・参照関係）.txt` - クラス設計
4. `docs/specs/4.実装タスク票.txt` - タスク定義
5. `docs/specs/5.受け入れテスト表.txt` - 完了基準
6. `docs/gap-analysis-2026-03-16.md` - 現状と仕様の乖離

## 現状（2026-03-16時点）

- MVP達成度: 約50%
- 根本的な乖離3件（詳細はgap-analysis参照）:
  1. 兵隊システム不在（HP=2の1体として実装。人数管理・配置・個体HPなし）
  2. ゲートの弾命中→値変化の仕組みがない
  3. 座標系の整合性要確認（Y軸移動 vs Z軸仕様）
- 不在ファイル: SoldierFormationController, SoldierUnit, GoalController, ItemPickup

## コーディングルール

- AGENTS.mdの共通ルールに従う（重複しないためここには書かない）
- 技術設計メモの責務分離・参照方向を守る
- `public API（MVP必須）`を優先。任意APIはスタブ可
- Inspector調整可能なパラメータは `[SerializeField] private` で公開
- 武器Lv上限は3（5ではない）
- 描画上限89人（中心1+4リング: 6/12/20/50）、内部カウントは上限なし

## ブランチ運用

- 命名: `feature_task-X-X`（例: `feature_task-4-2`）
- 基本はブランチを切ってPR経由でマージ
- PRレビュー: Claude Codeで即時レビュー → Codexで非同期セカンドレビュー（省略することあり）
- mainへの直接コミットは軽微な修正のみ

## テスト方針

- Unity Test Frameworkは未導入（導入予定あり: docs/specs/6.自動テスト方針メモ参照）
- 現時点の動作確認: Unityエディタで再生→受け入れテスト表の該当項目を目視確認
- 大きな変更前にはテスト項目を明確にしてから着手する

## シーン編集

- MCP For Unity（Fast MCP）を優先的に使用する（GameObject作成、Inspector設定、Prefab操作など）
- MCPで対応できない操作はEditor Scriptで自動化する
- いずれも困難な場合はInspector手順を明示し、ユーザーが手動で実施する

## 開発ペース

- 週5日稼働（木曜・日曜休みが多い）、1日8時間
- 1日1ブランチ（実装→動作確認→レビュー→マージ）を目標とする

## 作業終了ルーチン

ユーザーが「今日の作業終わり」等の終了メッセージを送ったら、以下を実行する:
1. `docs/mvp-task-plan-2026-03-16.md` の該当タスクに完了日と実工数を記録
2. 当日の作業で仕様変更が発生した場合、関連する仕様書（docs/specs/）を更新する
3. メモリに最新の進捗状態を保存（次の会話で即座に把握するため）
4. 翌稼働日の予定タスクを提示する
