# AI Evaluation Result - Claude Code

## 目的
AI 実装支援（Copilot / Cursor / Claude Code / Codex など）の評価結果を、あとで比較できる形で残す。

## 今回の評価対象
- AI: Claude Code
- ブランチ: ai-agent-setup-claude-code

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

- 実施日: 2026-03-13
- 実施AI: Claude Code
- 実施ブランチ: ai-agent-setup-claude-code
- 結果: Partial
- 点数目安: 8.5 / 10

### 実施内容
- Claude Code 拡張のセットアップ確認
- ③ゲート値更新の評価開発
  - 読み取り
  - 設計提案
  - 最小差分実装
  - 差分レビュー
  - レビュー指摘を受けた最小修正の再提案
- Edit Mode テスト追加の試行
- Unity Play 前提でのコンパイル復旧確認
- Scene 実体確認の必要性整理
- Unity MCP 再導入後の Scene 実体確認
- Gate 最小検証の追加実装
- Play による HP 2→1→2 の手動確認

### 変更ファイル（評価開発で触れたもの）
- Assets/Scripts/GateValueCalculator.cs
- Assets/Scripts/GateController.cs
- Assets/Scripts/Combat/BulletController.cs
- Assets/Scripts/PlayerGroupController.cs
- Assets/Tests/EditMode/GateValueCalculatorTests.cs
- Assets/Tests/EditMode/Tests.EditMode.asmdef
- Assets/Scripts/MainScripts.asmdef（途中提案・最終採用せず）

### 良かった点
- 読み取り精度が高く、課題の固定条件
  - 3D Trigger 前提
  - HP システム流用
  - GameManager / GameUIController 非変更
  - 1発で1変化
  - 負→0→正
  - 接触時に一度だけ反映
  を比較的よく踏まえて提案できた
- 設計提案では、案B（GateController + GateValueCalculator 分離）への寄せ方が自然で、今回の課題意図に最も合っていた
- GateValueCalculator を純ロジックとして切り出す判断は妥当で、責務分離もわかりやすかった
- BulletController への Gate 判定差し込み位置も概ね適切で、最小差分志向が強かった
- レビュー指摘後、
  - GetComponentInParent による誤反映リスク
  - 弾接触とプレイヤー接触の取り違え
  を理解し、最小修正案へ収束させる応答は良かった
- コード差分ベースでの説明能力が高く、なぜ修正が必要かを言語化できた
- 3者比較では、少なくともコード提案・局所修正・差分の寄せ方は最も良かった
- MCP 再導入後は、Scene / Hierarchy / Collider 構成の read-only 確認まで到達できた
- その結果を踏まえて、HP を使った Gate 最小検証案へ収束できた
- 最終的に Unity Play で
  - Gate_Minus1 通過で HP 2→1
  - Gate_Plus1 通過で HP 1→2
  を確認でき、Scene 実体確認込みでも最小検証を成立させられた

### 問題点
- Edit Mode テスト成立のための asmdef 対応で迷走があり、テストアセンブリ構成の理解は十分とは言えなかった
- `Tests.EditMode.asmdef` 追加は、実装上の必要性はあったが、許可対象外ファイル追加という点で指示遵守の小さな減点要素となった
- `MainScripts.asmdef` を追加してテスト参照問題を解こうとしたが、
  - TMPro
  - InputSystem
  など既存依存の参照不足を招き、Unity コンソールエラーを増やした
- 結果として、今回はテスト復旧よりも「まず Play 可能状態へ戻す」方針に切り替える必要があった
- Hierarchy / Prefab / Scene 上に Gate 実体が存在するかの確認が不足しており、コードだけでは Play 確認まで到達できなかった
- Scene 実体確認なしでは、`GateController.OnTriggerEnter` の妥当性を最終確定できなかった
- Unity Editor 内の実在構成を見ずに進めたため、最終的な完成度は人間確認前提となった

### 判定メモ
- Copilot より、読み取り精度・設計提案・最小差分志向・レビュー反映力は明確に良かった
- Codex と比べても、少なくともコード実装レベルでは Claude Code が最も本筋に沿っていた
- 特に
  - 仕様固定への追従
  - GateValueCalculator の切り出し
  - BulletController / GateController の責務整理
  は高評価
- MCP 再導入後は、Unity の Scene / Prefab / Inspector を含む実体確認と最小検証まで到達できた
- その結果、今回の Claude Code は、
  - コード実装評価としては最上位候補
  - Unity 実体確認込みでも、Copilot / Codex より前進できた
  と整理するのが妥当
- ただし、今回の Gate 実装はあくまで最小検証用であり、本番仕様としての完成度評価とは分けて考える必要がある

### 次回改善点
- 今回の最小検証結果を踏まえて、本番仕様に寄せる場合の追加実装範囲（人数ベース移行 / Prefab 化 / 配置見直し）を整理する
- 「コード評価」と「Scene 実体確認込み評価」を分けて記録する
- Edit Mode テスト評価では、asmdef 導入・依存参照・既存アセンブリ構成を事前に固定条件として与える
- 次回は Gate 実体の有無、Collider 構成、PlayerGroupController の配置を先に確認させる
- Unity Editor 上の存在確認を伴う課題では、MCP なし評価と MCP あり評価を分けて比較する

### 次回方針
- ai-agent-setup-claude-code ブランチは、現時点ではコード実装評価として一旦記録を残す
- ③ゲート値更新の最終マージ判断は保留とする
- MCP 再導入後に、
  - Scene 上の Gate 実体確認
  - Collider / Trigger 構成確認
  - Play 確認
  を含む再検証を別ラウンドで行う
- 再検証時は「コード実装はどこまで再利用できるか」も確認対象に含める

### 最終判断
- 採用したもの:
  - ③ゲート値更新の最小差分実装方針
  - GateValueCalculator 切り出し
  - GateController + GateValueCalculator 分離案
  - BulletController への Gate 判定追加方針
- 採用しなかったもの:
  - Edit Mode テストを通すための asmdef 導入案
  - MainScripts.asmdef を前提とした現時点のアセンブリ再構成
  - Scene 実体未確認のままでの最終マージ判断
- 理由:
  - Claude Code はコード実装の方向性として最も良く、MCP 再導入後の Scene 実体確認にも対応できた
  - 最終的に、Unity Play で HP 2→1→2 の最小検証を確認できた
  - ただし、今回は検証用の最小実装であり、本番仕様確定版とは分けて扱うべきため、評価結果は Partial を維持するのが妥当
