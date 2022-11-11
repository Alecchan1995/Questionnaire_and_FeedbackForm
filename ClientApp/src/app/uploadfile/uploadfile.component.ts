import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, SkipSelf, } from '@angular/core';
import { ReplaceTexts, AngularFileUploaderConfig, UploadInfo, UploadApi, } from './angular-file-uploader.types';
import { HttpClient, HttpHeaders, HttpParams, HttpEventType, } from '@angular/common/http';
@Component({
  selector: 'app-uploadfile',
  templateUrl: './uploadfile.component.html',
  styleUrls: ['./uploadfile.component.css']
})
export class UploadfileComponent implements OnInit {
  @Input()
  config!: AngularFileUploaderConfig;

  @Input()
  resetUpload = false;

  // Outputs
  @Output()
  ApiResponse = new EventEmitter();

  @Output()
  Send_file_to_feeback = new EventEmitter();

  @Output()
  fileSelected: EventEmitter<UploadInfo[]> = new EventEmitter<UploadInfo[]>();

  // Properties
  theme?: string;
  id?: number;
  hideProgressBar?: boolean;
  maxSize!: number;
  uploadAPI?: string;
  method?: string;
  formatsAllowed?: string;
  formatsAllowedText?: string;
  multiple?: boolean;
  headers?: HttpHeaders | { [header: string]: string | string[] };
  params?: HttpParams | { [param: string]: string | string[] };
  responseType?: 'json' | 'arraybuffer' | 'blob' | 'text';
  hideResetBtn!: boolean;
  hideSelectBtn!: boolean;
  allowedFiles: File[] = [];
  notAllowedFiles: {
    fileName: string;
    fileSize: string;
    errorMsg: string;
  }[] = [];
  Caption: string[] = [];
  Pic64:string[]=[];
  isAllowedFileSingle = true;
  progressBarShow = false;
  enableUploadBtn = false;
  uploadMsg = false;
  afterUpload = false;
  uploadStarted = false;
  uploadMsgText?: string;
  uploadMsgClass?: string;
  uploadPercent?: number;
  replaceTexts?: ReplaceTexts;
  currentUploads: any[] = [];
  fileNameIndex = true;
  withCredentials = false;
  autoUpload = false;

  private idDate: number = +new Date();
  constructor(@SkipSelf() private http: HttpClient) { }

  ngOnInit(): void {
  }
  ngOnChanges(changes: SimpleChanges) {
    // Track changes in Configuration and see if user has even provided Configuration.
    if (changes['config'] && this.config) {
      // Assign User Configurations to Library Properties.
      this.theme = this.config.theme || '';
      this.id =
        this.config.id ||
        parseInt((this.idDate / 10000).toString().split('.')[1], 10) +
        Math.floor(Math.random() * 20) * 10000;
      this.hideProgressBar = this.config.hideProgressBar || false;
      this.hideResetBtn = this.config.hideResetBtn || false;
      this.hideSelectBtn = this.config.hideSelectBtn || false;
      this.maxSize = (this.config.maxSize || 20) * 1024000; // mb to bytes.
      this.uploadAPI = this.config.uploadAPI.url;
      this.method = this.config.uploadAPI.method || 'POST';
      this.formatsAllowed = this.config.formatsAllowed || '*';
      this.formatsAllowedText =
        this.formatsAllowed === '*' ? '' : '(' + this.formatsAllowed + ')';
      this.multiple = this.config.multiple || false;
      this.headers = this.config.uploadAPI.headers || {};
      this.params = this.config.uploadAPI.params || {};
      this.responseType = this.config.uploadAPI.responseType || 'json';
      this.withCredentials = this.config.uploadAPI.withCredentials || false;
      this.fileNameIndex = this.config.fileNameIndex === false ? false : true;
      this.autoUpload = this.config.autoUpload || false;

      this.replaceTexts = {
        selectFileBtn: this.multiple ? 'Select Files' : 'Select File',
        resetBtn: 'Reset',
        uploadBtn: 'Upload',
        dragNDropBox: 'Drag N Drop',
        attachPinBtn: this.multiple ? 'Attach Files...' : 'Attach File...',
        afterUploadMsg_success: 'Successfully Uploaded !',
        afterUploadMsg_error: 'Upload Failed !',
        sizeLimit: 'Size Limit',
      }; // default replaceText.
      if (this.config.replaceTexts) {
        // updated replaceText if user has provided any.
        this.replaceTexts = {
          ...this.replaceTexts,
          ...this.config.replaceTexts,
        };
      }
    }
    if (changes['resetUpload']) {
      if (changes['resetUpload'].currentValue === true) {
        this.resetFileUpload();
      }
    }
  }
  resetFileUpload() {
    this.allowedFiles = [];
    this.Pic64 = [];
    this.Caption = [];
    this.notAllowedFiles = [];
    this.uploadMsg = false;
    this.enableUploadBtn = false;
    this.Send_file_to_feeback.emit(this.allowedFiles);
  }
  onChange(event: any) {
    console.log("upload file:",event);
    console.log("=========",event.target.result);
    this.fileSelected.emit(event);
    this.notAllowedFiles = [];
    const fileExtRegExp: RegExp = /(?:\.([^.]+))?$/;
    let fileList: FileList ; //      interface FileList {
                              //     readonly length: number;
                              //     item(index: number): File | null;
                              //     [index: number]: File;
                              // }
    if (this.afterUpload || !this.multiple) {
      this.allowedFiles = [];
      this.Pic64 = [];
      this.Caption = [];
      this.afterUpload = false;
    }

    if (event.type === 'drop') {
      fileList = event.dataTransfer.files;
    } else {
      fileList = event.target.files || event.srcElement.files;
    }

    // 'forEach' does not exist on 'filelist' that's why this good old 'for' is used.
    for (let i = 0; i < fileList.length; i++) {
      const currentFileExt = fileExtRegExp.exec(fileList[i].name)![1].toLowerCase(); // Get file extension.
      const isFormatValid = this.formatsAllowed!.includes('*')? true
        : this.formatsAllowed!.includes(currentFileExt);

      const isSizeValid = fileList[i].size <= this.maxSize!;

      // Check whether current file format and size is correct as specified in the configurations.
      if (isFormatValid && isSizeValid) {
        this.allowedFiles.push(fileList[i]); //是存file地方
      } else {
        this.notAllowedFiles.push({
          fileName: fileList[i].name,
          fileSize: this.convertSize(fileList[i].size),
          errorMsg: !isFormatValid ? 'Invalid format' : 'Invalid size',
        });
      }
    }

    // If there's any allowedFiles.
    if (this.allowedFiles.length > 0) {
      this.enableUploadBtn = true; // selected的按鈕
      // Upload the files directly if theme is attach pin (as upload btn is not there for this theme) or autoUpload is true.
      if (this.theme === 'attachPin' || this.autoUpload) { //auto 上傳
        //this.uploadFiles();
      }
    } else {
      this.enableUploadBtn = false;
    }

    this.uploadMsg = false;
    this.uploadStarted = false;
    this.uploadPercent = 0;
    event.target.value = null;
    this.Send_file_to_feeback.emit(this.allowedFiles);
    // this.allowedFiles.forEach(filename =>{
    //   var reader = new FileReader(); //建立FileReader物件
    //   var dataURL = "";
    //   reader.readAsDataURL(filename);
    //   reader.onload = function(){
    //     dataURL = reader.result!.toString();
    //     console.log("dataURL:",dataURL);
    //     var img = document.createElement('img');
    //     img.src = dataURL!.toString();
    //     document.getElementById(filename.name)!.innerHTML += "<br/>";
    //     document.getElementById(filename.name)!.appendChild(img);
    //   }
    //   //this.Pic64.push(dataURL);
    // });
    //console.log("===========",this.Pic64);
    // reader.readAsDataURL(this.allowedFiles[0]); //以.readAsDataURL將上傳檔案轉換為base64字串

    // reader.onload = function(){ //FileReader取得上傳檔案後執行以下內容
    //   var dataURL = reader.result; //設定變數dataURL為上傳圖檔的base64字串
    //   console.log("dataURL",dataURL,typeof dataURL);
    //   //var img = document.createElement('img');
    //   //img.src = dataURL!.toString();
    //   //document.getElementById('file_name')!.appendChild(img);
    //   //$('#output').attr('src', dataURL).show(); //將img的src設定為dataURL並顯示
    // };

  }
  openpic(file:File,idname:string){
    if(document.body.contains(document.getElementById(idname+"pic"))){
      document.getElementById(idname+"pic")?.remove();
      document.getElementById(idname+"br")?.remove();
  } else{
      var reader = new FileReader(); //建立FileReader物件
      reader.readAsDataURL(file);
      reader.onload = function(){
        console.log("reader.result!.toString() :",reader.result!.toString());
        var img = document.createElement('img');
        if(reader.result!.toString().length > 5){img.src = reader.result!.toString();}
        img.id = idname+"pic"
        img.alt = "This isn't picture"
        document.getElementById(idname)!.innerHTML += "<br id='"+idname+"br' />";
        document.getElementById(idname)!.appendChild(img);
      }
    }
  }
  convertSize(fileSize: number): string {
    return fileSize < 1024000
      ? (fileSize / 1024).toFixed(2) + ' KB'
      : (fileSize / 1024000).toFixed(2) + ' MB';
  }
  removeFile(i: any, sf_na: any) {
    if (sf_na === 'sf') {
      this.allowedFiles.splice(i, 1);
      this.Caption.splice(i, 1);
    } else {
      this.notAllowedFiles.splice(i, 1);
    }

    if (this.allowedFiles.length === 0) {
      this.enableUploadBtn = false;
    }
  }

  attachpinOnclick() {
    const element = document.getElementById('sel' + this.id);
    if (element !== null) {
      element.click();
    }
  }
  uploadFiles() {
    this.progressBarShow = true;
    this.uploadStarted = true;
    this.notAllowedFiles = [];
    let isError = false;
    this.isAllowedFileSingle = this.allowedFiles.length <= 1;
    const formData = new FormData();

    // Add data to be sent in this request
    this.allowedFiles.forEach((file, i) => {
      formData.append(
        this.Caption[i] || 'file' + (this.fileNameIndex ? i : ''),  //接C#後端的inface
        this.allowedFiles[i]
      ) ;
    });

    /*
    Not Working, Headers null
    // Contruct Headers
    const headers = new HttpHeaders();
    for (const key of Object.keys(this.headers)) {
      headers.append(key, this.headers[key]);
    }
    // Contruct Params
    const params = new HttpParams();
    for (const key of Object.keys(this.params)) {
      params.append(key, this.params[key]);
    } */

    this.http
      .request(this.method!.toUpperCase(), this.uploadAPI!, {
        body: formData,
        reportProgress: true,
        observe: 'events',
        headers: this.headers,
        params: this.params,
        responseType: this.responseType,
        withCredentials: this.withCredentials,
      })
      .subscribe(
        (event) => {
          // Upload Progress
          if (event.type === HttpEventType.UploadProgress) {
            this.enableUploadBtn = false; // button should be disabled if process uploading
            const currentDone = event.loaded / event.total;
            this.uploadPercent = Math.round((event.loaded / event.total) * 100);
          } else if (event.type === HttpEventType.Response) {
            if (event.status === 200 || event.status === 201) {
              // Success
              this.progressBarShow = false;
              this.enableUploadBtn = false;
              this.uploadMsg = true;
              this.afterUpload = true;
              if (!isError) {
                this.uploadMsgText = this.replaceTexts!.afterUploadMsg_success;
                this.uploadMsgClass = 'text-success lead';
              }
            } else {
              // Failure
              isError = true;
              this.handleErrors();
            }

            this.ApiResponse.emit(event);
          } else {
          }
        },
        (error) => {
          // Failure
          isError = true;
          this.handleErrors();
          this.ApiResponse.emit(error);
        }
      );
  }
  drop(event: any) {
    event.stopPropagation();
    event.preventDefault();
    this.onChange(event);
  }

  allowDrop(event: any) {
    event.stopPropagation();
    event.preventDefault();
    event.dataTransfer.dropEffect = 'copy';
  }

  handleErrors() {
    this.progressBarShow = false;
    this.enableUploadBtn = false;
    this.uploadMsg = true;
    this.afterUpload = true;
    this.uploadMsgText = this.replaceTexts!.afterUploadMsg_error;
    this.uploadMsgClass = 'text-danger lead';
  }

}
