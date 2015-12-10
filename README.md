CSharp-Sample
=============

新しいサンプルを追加する時は、

```
git checkout ffac5af -b <ブランチ名>
```

として、initial commitからブランチを作成しています。

　  
また、ブランチのみ取得する場合は、ローカルブランチを「development」、リモートブランチを「origin/development」とすると、

```
git checkout -b development origin/development
```

となります。

参考：[Git - リモートのブランチをcloneする - Qiita](http://qiita.com/shim0mura/items/85aa7fc762112189bd73)

　  
また、これらのサンプルでは`Microsoft.TeamFoundation.MVVM`名前空間を使っているものもありますが、公式Blogに以下の記事が掲載されました。そのため、`Microsoft.TeamFoundation.MVVM`の使用前に記事を確認してみてください。  
[Microsoft.TeamFoundation.MVVM 名前空間の利用について - Visual Studio サポート チーム blog - Site Home - MSDN Blogs](http://blogs.msdn.com/b/jpvsblog/archive/2015/12/07/mvvm.aspx)
