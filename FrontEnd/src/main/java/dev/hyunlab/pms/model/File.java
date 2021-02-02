package dev.hyunlab.pms.model;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class File {
    @JsonProperty(value = "Name")
    private String name;
    @JsonProperty(value = "Size")
    private long size;
    @JsonProperty(value = "IsDirectory")
    private boolean isDirectory;
    
    @JsonProperty(value = "Date")
    private String date;
}
