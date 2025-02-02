# UnityEditorAnimationClipPathReplaceTool

AnimationClip は対象へのパスを文字列として保存しており、オブジェクト名を変更する際にすべてのパスを書き換える必要があるため、面倒な作業を自動化するツールとして作成しました。

<img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20200830/20200830014234.png" width="300" alt="AnimationClip Path Replace Tool">

---

## 使い方

1. **ウィンドウの起動**  
   メニューの `Tools/ReplaceAnimationClipPath` からウィンドウを開きます。

2. **対象 AnimationClip の設定**  
   `Target AnimationClip` に対象の AnimationClip をアタッチすると、その AnimationClip に保存されているパスの一覧が下部に表示されます。

   <img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20200830/20200830014428.png" width="300" alt="AnimationClip Path List">

3. **パスの変更**  
   変更したいパスを一覧からクリックすると、上部に変更用の入力欄が表示されます。  
   ここに新しいパスを入力し、`Replace` ボタンを押すとパスが置換されます。

   <img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20200830/20200830014514.png" width="300" alt="Replace Path">

   例えば、「GameObject (1)」というパスを「Root」に変更することが可能です。
