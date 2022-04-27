# blazor-e-commerce
 
本リポジトリは、Blazor で作成した E コマースサイトのサンプルプログラムです。  
動作イメージは以下の動画で確認できます。  
https://youtu.be/MPsrhUBdK3c

## 解説書

本リポジトリの詳しい解説書を執筆しました。  
https://nekojoker.booth.pm/items/3813171

## 実装されている機能

- ログイン
- 商品（一覧表示、個別ページ）
- カート
- 決済
- 注文履歴
- レビュー（一覧表示、新規、修正、削除）
- ユーザープロフィール（参照、修正）

## 使用されている主な技術やツール

- ASP.NET Core Blazor WebAssembly
- Entity Framework
- Azure Active Directory B2C
- Microsoft Graph
- Local Storage
- Stripe (.NET, Webhook)
- Swagger

## 事前準備

本リポジトリは、そのままでは正常に動作しません。
事前準備として、以下の`appsettings.json`に記載している環境変数の設定が必要です。  

- `Client/wwwroot/appsettings.json` 
- `Server/appsettings.json` 

`{your-value}` としている部分に実際の値を埋めてください。 
何の値を設定すべきか不明な場合は、公式ドキュメントや解説書を参照してください。

## 実行

以下のコマンドを使用してデータベースを更新したあと、デバッグ実行します。

```
// データベースの更新
dotnet ef database update
```

## Swagger の起動

デバッグ実行後、以下の URL から Swagger を起動できます。

```
https://localhost:5001/swagger/index.html
```

## 作成者情報

ねこじょーかー [@nekojoker1234](https://twitter.com/nekojoker1234)  
[Blazorのブログ](https://blazor-master.com/)でも発信中。
