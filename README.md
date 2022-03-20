# blazor-e-commerce
 
本リポジトリは、Blazor で作成した E コマースサイトのサンプルプログラムです。  
動作イメージは以下の動画で確認できます。  
https://youtu.be/g3i_23BQEI4

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
事前準備として、`appsettings.json`に記載している環境変数の設定が必要です。  
`{your-value}` としている部分に実際の値を埋めてください。

`Client/wwwroot/appsettings.json` は以下の内容になります。

|プロパティ|設定値|
|----|----|
|AzureAdB2C:Authority|サインインのユーザーフロー URL|
|AzureAdB2C:ClientId|クライアントアプリのクライアント ID|
|AzureAdB2C:DefaultScope|スコープの URL|

`Server/appsettings.json` は以下の内容になります。

|プロパティ|設定値|
|----|----|
|AzureAdB2C:Instance|インスタンス名|
|AzureAdB2C:ClientId|サーバーアプリのクライアント ID|
|AzureAdB2C:Domain|ドメイン名|
|AzureAdB2C:Scopes|スコープ名|
|AzureAdB2C:SignUpSignInPolicyId|サインインのユーザーフロー名|
|AzureAdB2C:TenantId|テナント ID|
|AzureAdB2C:ClientSecret|サーバーアプリのクライアントシークレット|
|Stripe:ApiKey|Stripe の API キー|
|Stripe:WebhookSecret|Stripe の Webhook シークレット|
|ConnectionStrings:DefaultConnection|SQL Server の接続文字列|
|Swagger:ClientId|Azure AD B2C の Swagger 用アプリのクライアント ID|
|Swagger:Scopes|Azure AD B2C の Swagger 用アプリのスコープ名|

Azure 関連は[こちら](https://blazor-master.com/azure-active-directory-b2c/)、Stripe 関連は[こちら](https://blazor-master.com/stripe/)でもう少し詳しく解説しています。  
何の値を設定すべきか不明な場合は、公式ドキュメントもあわせて参照してください。

## 実行

以下のコマンドを使用してデータベースを更新したあと、デバッグ実行します。

```
// dotnet コマンドをインストールしていない人のみ
dotnet tool install --global dotnet-ef

// データベースの更新
dotnet ef database update
```
## Swagger の起動

デバッグ実行後、以下の URL から Swagger を起動できます。

```
https://localhost:7030/swagger/index.html
```

## 作成者情報

ねこじょーかー [@nekojoker1234](https://twitter.com/nekojoker1234)  
[Blazorのブログ](https://blazor-master.com/)でも発信中。
