import os

if __name__ == '__main__':
    
    error_code_message_resource_file_path = '../EnadlaInventory.UI/Strings/CodeErrorMessages.resx'
    error_code_message_converter_path = '../EnadlaInventory.UI/Helpers/Mvvm/ErrorCodeMessagesKeyConverter.cs'
    namespace_for_converter = 'EnadlaInventory.UI.Helpers.Mvvm'
    error_code_message_resource_full_name_class = 'EnadlaInventory.UI.Strings.CodeErrorMessages'
    
    command = f'ResourceDynamycConverterCreator\\ResourceDynamicConverterCreator.py ' + \
              f'"{os.path.abspath(error_code_message_resource_file_path)}" ' + \
              f'"{os.path.abspath(error_code_message_converter_path)}" ' + \
              f'"{namespace_for_converter}" ' + \
              f'"{error_code_message_resource_full_name_class}"'
                

    os.system(command)