# Minimal example

[Forum entry](https://forum.gemboxsoftware.com/t/remote-images-not-working-in-docker/1173)

Including an image using a link in GemBox.Document will lead to an error.

Used example: [Create Word (DOCX) or PDF in Docker container](https://www.gemboxsoftware.com/document/examples/create-word-pdf-on-docker-net-core/5902)

I found changing the dependencies from `SkiaSharp.NativeAssets.Linux` to `SkiaSharp.NativeAssets.Linux.NoDependencies` will solve the problem.

See the misc controller for more details.